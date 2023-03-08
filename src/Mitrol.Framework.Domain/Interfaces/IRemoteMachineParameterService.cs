namespace Mitrol.Framework.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;

    public interface IRemoteMachineParameterService
    {
        MachineParameterItem Get(string code, MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem);
        decimal GetValue(ParameterCategoryEnum parameterCategory, int index, MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem);
        Result SetCNCVariables(CncTypeEnum cncTypeFilter = CncTypeEnum.All);
    }
}
