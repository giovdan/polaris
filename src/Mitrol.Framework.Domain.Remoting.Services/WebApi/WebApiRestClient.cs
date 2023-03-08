namespace Mitrol.Framework.Domain.Remoting.Services.WebApi
{
    using Mitrol.Framework.Domain.Remoting.Services.Configuration;
    using Microsoft.Extensions.Configuration;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System.Runtime.InteropServices;
    using System;
    using Mitrol.Framework.Domain.Bus.Events;

    public class WebApiRestClient
    {
        private static readonly object s_syncRoot = new object();

        private readonly IConfigurationRoot _configurationRoot;
        private readonly IServiceFactory _serviceFactory;

        public WebApiRestClient(IServiceFactory serviceFactory)
        {
            _configurationRoot = serviceFactory.GetService<IConfigurationRoot>();
            _serviceFactory = serviceFactory;
        }

        public StartApplicationSetupSection SetupSection
        {
            get
            {
                lock (s_syncRoot)
                {
                    if (_setupSection == null)
                    {
                        _setupSection = new StartApplicationSetupSection(_configurationRoot);
                    }
                }
                return _setupSection;
            }
        }
        private StartApplicationSetupSection _setupSection;

        public BootSection BootSection
        {
            get
            {
                if (_bootSection == null)
                {
                    _bootSection = new BootSection();
                    _configurationRoot.GetSection("Boot").Bind(_bootSection);

                    _bootSection.User.GrantType = GrantTypeEnum.HashedPassword;
                }
                return _bootSection;
            }
        }
        private BootSection _bootSection;

        public Result<IUserSession> Login(LoginRequest loginRequest)
        {
            return WebApiCaller.Login(loginRequest, 15000)
                .OnSuccess(userSession => UserSession = userSession);
        }

        public void Logout()
        {
            WebApiCaller.Post(new WebApiRequest(UserSession)
            {
                Uri = $"users/logout",
            });

            UserSession = null;
        }

        public IUserSession UserSession
        {
            get
            {
                lock (s_syncRoot)
                {
                    if (_userSession == null)
                    {
                        var bootLoginResult = WebApiCaller.Login(BootSection.User, 15000);
                        if (bootLoginResult.Failure)
                        {
                            _serviceFactory.GetService<IProgressEventCollection>()
                                .Add(new ProgressEvent(bootLoginResult.Errors.ToErrorString(), Core.Enums.EventTypeEnum.Error));
                        }
                        else
                        {
                            _userSession = bootLoginResult.Value;
                        }
                    }
                    return _userSession;
                }
            }
            internal set
            {
                _userSession = value;
                UserSessionChanged?.Invoke(this, value);
            }
        }
        private IUserSession _userSession;

        public WebApiCaller WebApiCaller
        {
            get
            {
                if (_webApiCaller == null)
                    _webApiCaller = new WebApiCaller(SetupSection.BaseUri, SetupSection.ApiVersion);
                return _webApiCaller;
            }
        }

        public EventHandler<IUserSession> UserSessionChanged { get; set; }

        private WebApiCaller _webApiCaller;
    }
}
