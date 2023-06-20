namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Enums;

    public class MachineParameterFilter
    {
        public string Code { get; set; }
        public MeasurementSystemEnum ConversionSystem { get; set; }
    }
}
