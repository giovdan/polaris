namespace Mitrol.Framework.Domain.Remoting.Fanuc
{
    using Mitrol.Framework.Domain.Configuration.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Remoting.Services.WebApi;
    using System.Collections.Generic;
    using System.Linq;

    public static class Common
    {
        private static readonly object s_syncRoot = new object();

        public static WebApiCaller WebApiCaller
        {
            get
            {
                if (s_webApiCaller == null)
                    s_webApiCaller = new WebApiCaller(ApplicationConfiguration.SetupSection.BaseUri
                        , ApplicationConfiguration.SetupSection.ApiVersion);
                return s_webApiCaller;
            }
        }
        private static WebApiCaller s_webApiCaller;

        public static IUserSession UserSession
        {
            get
            {
                lock (s_syncRoot)
                {
                    if (s_userSession == null)
                    {
                        s_userSession = BootLogin();
                    }
                    return s_userSession;
                }
            }
            internal set => s_userSession = value;
        }
        private static IUserSession s_userSession;

        private static IUserSession BootLogin()
        {
            ApplicationConfiguration.BootSection.User.GrantType = GrantTypeEnum.HashedPassword;
            var result = WebApiCaller.Login(ApplicationConfiguration.BootSection.User);

            return result.Success ? result.Value : NullUserSession.Instance;
        }

        public static void Logout()
        {
            WebApiCaller.Post(new WebApiRequest(UserSession)
            {
                Uri = $"users/logout",
            });
        }
        public static void RemoveEvents()
        {
            WebApiCaller.Delete(new WebApiRequest(UserSession)
            {
                Uri = $"eventlogs",
            });
        }

        public static string FanucIpAddress
        {
            get
            {
                //if (s_fanucIpAddress == null)
                //{
                //    s_fanucIpAddress = MachineConfiguration
                //        .Get(ConfigurationContextEnum.Fanuc, ConfigurationKeyEnum.IpAddress)
                //        ?.Value;
                //}
                return s_fanucIpAddress;
            }
        }
        private static string s_fanucIpAddress;

        public static string FanucIpPort
        {
            get
            {
                //if (s_fanucIpPort == null)
                //{
                //    s_fanucIpPort = MachineConfiguration
                //        .Get(ConfigurationContextEnum.Fanuc, ConfigurationKeyEnum.Port)
                //        ?.Value ?? "8193";
                //}
                return s_fanucIpPort;
            }
        }
        private static string s_fanucIpPort;
    }
}
