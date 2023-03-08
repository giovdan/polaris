namespace Mitrol.Framework.Domain.Remoting.Services
{
    using Mitrol.Framework.Domain.Configuration;
    using Mitrol.Framework.Domain.Configuration.Enums;
    using Mitrol.Framework.Domain.Configuration.Interfaces;
    using Mitrol.Framework.Domain.Configuration.Models;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Macro;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Production.Models;
    using Mitrol.Framework.Domain.Remoting.Services.WebApi;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class RemoteMachineConfigurationService : IRemoteMachineConfigurationService
    {
        public WebApiCaller WebApiCaller { get; }
        public IUserSession UserSession { get; }

        public IDictionary<ApplicationSettingKeyEnum, string> ApplicationSettings
        {
            get
            {
                if (_applicationSettings == null)
                {
                    //{{url}}/api/v1/OData/Machine/ApplicationSettings
                    _applicationSettings = WebApiCaller
                        .GetAll<ApplicationSettingItem>("OData/Machine/ApplicationSettings", UserSession)
                        ?.ToDictionary(item => item.SettingKey, item => item.Value);
                }
                return _applicationSettings;
            }
        }
        private IDictionary<ApplicationSettingKeyEnum, string> _applicationSettings;

        public IRootConfiguration ConfigurationRoot { get; private set; }

        public RemoteMachineConfigurationService(WebApiRestClient remoteData)
        {
            WebApiCaller = remoteData.WebApiCaller;
            UserSession = remoteData.UserSession;

            var response = WebApiCaller.Get<RootConfiguration>(new WebApiRequest(UserSession)
            {
                Uri = $"machineConfiguration",
                IsOldStyle = false,
            })
            .OnSuccess(configurationRoot =>
            {
                ConfigurationRoot = configurationRoot;
            });
        }

        public string GetApplicationSettingValue(ApplicationSettingKeyEnum applicationSettingKey)
        {
            if (ApplicationSettings.TryGetValue(applicationSettingKey, out var value)) { }

            return value;
        }

        public bool GetMacroTypeEnabled(MacroTypeEnum type)
        {
            var response = WebApiCaller.Get<bool>(new WebApiRequest(UserSession)
            {
                Uri = $"machine/macroEnabled/{type}",
            });
            return response.Value;
        }

        public bool GetMacroEnabled(MacroConfigurationFilter macroConfigurationFilter)
        {
            var response = WebApiCaller.Patch(new WebApiRequest<MacroConfigurationFilter>(UserSession)
            {
                Uri = $"machine/macroEnabled/Macro",
                Model = macroConfigurationFilter

            });
            return JsonConvert.DeserializeObject<bool>(response.JsonResult);
        }

        public FontConfiguration GetFontConfiguration(FontConfigurationFilter filter)
        {
            var response = WebApiCaller.Patch(new WebApiRequest<FontConfigurationFilter>(UserSession)
            {
                Uri = $"machine/fontConfiguration/filter",
                Model = filter

            });
            return JsonConvert.DeserializeObject<FontConfiguration>(response.JsonResult);

        }
    }
}
