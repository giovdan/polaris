namespace Mitrol.Framework.Domain.Core.Helpers
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
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
            Initialize(jsonAppSettings,env.EnvironmentName, externalConfig);
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
                string directory = CoreExtensions.GetConfigDirectory()?.FullName ?? Directory.GetCurrentDirectory();
                var builder = new ConfigurationBuilder()
                    .SetBasePath(directory)
                    .AddJsonFile($"{jsonAppSettings}.json", optional: false, reloadOnChange: true)
                    .AddJsonFile($"{jsonAppSettings}.{environmentName}.json", optional: false, reloadOnChange: true);
                if (!string.IsNullOrEmpty(externalConfigFile))
                    builder = builder.AddJsonFile($"{externalConfigFile}.{environmentName}.json", optional: false, reloadOnChange: true);

                builder = builder.AddEnvironmentVariables();
                Initialize(builder.Build());
            }
        }

        public IConfigurationSection GetConfigurationSection(string name)
        {
            return Instance.Configuration.GetSection(name);
        }

        public string GetConnectionString<T>(string dbName, bool useMySQL = false) where T : DbContext
        {
            if (!useMySQL)
            {//Default SQLite connection
                string assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                string solutionLocation = DomainExtensions.GetStartUpDirectoryInfo(assemblyLocation).FullName;
                var optionsBuilder = new DbContextOptionsBuilder<T>();
                return Instance.Configuration.GetConnectionString(dbName).Replace("|DataDirectory|", solutionLocation);
            }
            else
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

        public Result<UserSession> ResetCurrentSession(HttpContext context)
        {
            var nullUserSession = NullUserSession.Instance;

            var userData = context.User.Identities.FirstOrDefault().Claims
                    .SingleOrDefault(x => x.Type == ClaimTypes.UserData);

            if (userData != null && context.User.Identities.FirstOrDefault().TryRemoveClaim(userData))
            {
                context.User.Identities.FirstOrDefault().AddClaim(new Claim(ClaimTypes.UserData.ToString()
                                , JsonConvert.SerializeObject(nullUserSession)));
            }

            return Result.Ok(nullUserSession as UserSession);
        }

        public static void SetAuthenticationSpecs(IServiceCollection services)
        {
            var audienceConfig = Instance.Configuration.GetSection("TokenAuthentication");

            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(audienceConfig["Secret"]));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,
                ValidateIssuer = true,
                ValidIssuer = audienceConfig["Issuer"],
                ValidateAudience = true,
                ValidAudience = audienceConfig["Audience"],
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                RequireExpirationTime = true,
            };

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = "ApiSecurity";
            })
            .AddJwtBearer("ApiSecurity", options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = tokenValidationParameters;

                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}