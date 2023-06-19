namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class ToolMastersFilterItemValidator: AbstractValidator<ToolMastersFilterItem>
    {
        public ToolMastersFilterItemValidator()
        {
            RuleFor(x => x.PlantUnit)
                .IsInEnum()
                .WithErrorCode(ErrorCodesEnum.ERR_TLM008.ToString());

            RuleFor(x => x.ConversionSystem)
                .IsInEnum()
                .WithErrorCode(ErrorCodesEnum.ERR_GEN010.ToString());

        }
    }

    public class ToolMastersFilterItem
    {
        [JsonProperty("PlantUnit")]
        public PlantUnitEnum PlantUnit { get; set; }
        [JsonIgnore()]
        public MeasurementSystemEnum ConversionSystem { get; set; }
        [JsonProperty("IsRelatedToTables")]
        public bool IsRelatedToTables { get; set; }

        public ToolMastersFilterItem()
        {
            PlantUnit = PlantUnitEnum.All;
            ConversionSystem = MeasurementSystemEnum.MetricSystem;
            IsRelatedToTables = true;
        }
    }
}
