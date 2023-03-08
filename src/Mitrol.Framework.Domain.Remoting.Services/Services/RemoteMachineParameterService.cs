namespace Mitrol.Framework.Domain.Remoting.Services
{
    using Mitrol.Framework.Domain.Remoting.Services.WebApi;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mitrol.Framework.Domain.Remoting.Services.Extensions;

    public class RemoteMachineParameterService : IRemoteMachineParameterService
    {
        public WebApiCaller WebApiCaller { get; }

        public IUserSession UserSession { get; }

        public Dictionary<ValueTuple<ParameterCategoryEnum, long>, MachineParameterItem> MachineParameters
        {
            get
            {
                if (s_machineParameters == null || s_machineParameters.Count() == 0)
                {
                    var x = WebApiCaller
                        .GetAll<MachineParameterItem>($"OData/Machine/Parameters", UserSession);

                    s_machineParameters = x.ToDictionary(par => (par.Category, par.Id), par => par);
                }
                return s_machineParameters;
            }
        }
        private static Dictionary<ValueTuple<ParameterCategoryEnum, long>, MachineParameterItem> s_machineParameters;

        public RemoteMachineParameterService(WebApiRestClient remoteData)
        {
            WebApiCaller = remoteData.WebApiCaller;
            UserSession = remoteData.UserSession;
        }

        public decimal GetValue(ParameterCategoryEnum parameterCategory, int index, MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem)
        {
            if (MachineParameters.TryGetValue((parameterCategory, index), out var parameter)) { }

            return parameter?.Value ?? 0;
        }

        public MachineParameterItem Get(string code, MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem)
        {
            return MachineParameters.Values.SingleOrDefault(p => p.Code == code);
        }

        public Result SetCNCVariables(CncTypeEnum cncType)
        {
            var request = new WebApiRequest(UserSession)
            {
                IsOldStyle = false,
                Uri = $"MachineParameters/setCncVariables/{cncType}",
            };
            return WebApiCaller.Post(request).ToResult();
        }
    }
}
