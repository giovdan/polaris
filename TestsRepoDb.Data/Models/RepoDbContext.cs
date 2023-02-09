namespace RepoDbVsEF.Data.Models
{
    using Microsoft.Extensions.Configuration;
    using MySql.Data.MySqlClient;
    using RepoDb;
    using System.Data;
    using RepoDbVsEF.Data.Interfaces;
    using RepoDbVsEF.Domain;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.Domain.Models.Core;
    using global::RepoDb;

    public class RepoDbContext: IRepoDbDatabaseContext
    {
        private const string DEFAULT_USERNAME = "MITROL";

        public IUserSession UserSession { get; private set; }

        public IDbConnection Connection { get; private set; }

        private string ConnectionString
        {
            get
            {
                var mySQLSection = new MySQLSection();
                DomainExtensions.GetConfiguration().GetSection("MySQL").Bind(mySQLSection);
                return
                 ($"Server={mySQLSection.Server};port={mySQLSection.Port};Database={mySQLSection.Database};Uid={mySQLSection.Username};Pwd={SimpleStringCipher.Instance.Decrypt(mySQLSection.Password)}");

            }
        }


        public RepoDbContext(IServiceFactory serviceFactory)
        {
            GlobalConfiguration
                .Setup()
                .UseMySql();

            //ClassMapper.Add<Customer>("Customer");
            //ClassMapper.Add<Product>("Product");
            //ClassMapper.Add<Supplier>("Supplier");
            Connection = MySqlClientFactory.Instance.CreateConnection();
            Connection.ConnectionString = ConnectionString;
        }

        /// <summary>
        /// Get current logged User from Session
        /// </summary>
        /// <returns></returns>
        public virtual string GetUserNameToLog()
        {
            if (!string.IsNullOrEmpty(UserSession?.Username))
            {
                return UserSession.Username;
            }

            return DEFAULT_USERNAME;
        }


        public void SetSession(IUserSession session)
        {
            UserSession = session;
        }

        public void Dispose()
        {
            Connection.Dispose();
            UserSession = null;
            Connection = null;
        }
    }
}
