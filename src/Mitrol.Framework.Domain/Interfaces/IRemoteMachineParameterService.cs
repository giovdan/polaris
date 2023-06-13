namespace Mitrol.Framework.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using System.Collections.Generic;

    public interface IRemoteMachineParameterService: IApplicationService
    {
        MachineParameterItem Get(string code, MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem);
        decimal GetValue(ParameterCategoryEnum parameterCategory, int index, MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem);
        Result SetCNCVariables(CncTypeEnum cncTypeFilter = CncTypeEnum.All);
        Dictionary<string, decimal> GetLinearNestingParameters();
    }
}
