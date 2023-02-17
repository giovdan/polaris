namespace RepoDbVsEF.Domain.Helpers
{
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using RepoDbVsEF.Domain.Models;
    using RepoDbVsEF.Domain.Models.Core;
    using System;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    public class WebApiHelper
    {
        public IConfiguration Configuration { get; private set; }
        public static WebApiHelper Instance { get; private set; }

        public static void Initialize(IConfiguration configuration)
        {
            Instance = new WebApiHelper
            {
                Configuration = configuration
            };
        }

        public static void Initialize(string jsonAppSettings, IHostingEnvironment env, string externalConfig = null)
        {
            Initialize(jsonAppSettings, env.EnvironmentName, externalConfig);
        }


        /// <summary>
        /// Initialize Configuration 
        /// </summary>
        /// <param name="jsonAppSettings"></param>
        /// <param name="environmentName"></param>
        /// /// <param name="externalConfigFile"></param>
        public static void Initialize(string jsonAppSettings, string environmentName, string externalConfigFile = null)
        {
            if (Instance == null)
            {
                string directory = DomainExtensions.GetConfigDirectory()?.FullName ?? Directory.GetCurrentDirectory();
                var builder = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile($"{jsonAppSettings}.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"{jsonAppSettings}.{environmentName}.json", optional: false, reloadOnChange: true);
                if (!string.IsNullOrEmpty(externalConfigFile))
                    builder = builder.AddJsonFile($"{externalConfigFile}.{environmentName}.json", optional: false, reloadOnChange: true);

                Initialize(builder.Build());
            }
        }

        public IConfigurationSection GetConfigurationSection(string name)
        {
            return Instance.Configuration.GetSection(name);
        }

        public string GetConnectionString<T>(string dbName) where T : DbContext
        {

            var mySQLSection = new MySQLSection();
            DomainExtensions.GetConfiguration().GetSection("MySQL").Bind(mySQLSection);
            return Instance.Configuration.GetConnectionString(dbName);
        }


        public UserSession GetCurrentSession(HttpContext context)
        {
            var userData = context.User.Identities.FirstOrDefault().Claims
                                .SingleOrDefault(x => x.Type == ClaimTypes.UserData)?.Value;

            return string.IsNullOrEmpty(userData) ? NullUserSession.Instance
                            : JsonConvert.DeserializeObject<UserSession>(userData)
                            ?? NullUserSession.Instance;
        }



        
    }
}