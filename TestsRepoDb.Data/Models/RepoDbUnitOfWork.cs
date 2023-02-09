namespace RepoDbVsEF.Data.Models
{
    using System;
    using System.Data;
    using RepoDbVsEF.Data.Interfaces;
    using RepoDbVsEF.Domain.Interfaces;

    public class RepoDbUnitOfWork : IUnitOfWork<IRepoDbDatabaseContext>
    {
        public IRepoDbDatabaseContext Context { get; set; }

        public bool IsFinished { get; set; }

        public string Id { get; set; }

        public IUserSession UserSession { get; set; }
        public IDbTransaction CurrentTransaction { get; set; }

        public RepoDbUnitOfWork(IServiceFactory serviceFactory)
        {
            UserSession = null;
            Context = new RepoDbContext(serviceFactory);
            Id = Guid.NewGuid().ToString();
        }
        
   

        public void Dispose()
        {
            if (IsFinished)
            {
                if (Context.Connection.State == System.Data.ConnectionState.Open)
                {
                    Context.Connection.Close();
                }

                Context.Dispose();
            }
        }

        public void Commit()
        {
            
        }
    }
}
