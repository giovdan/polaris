namespace Mitrol.Framework.MachineManagement.Application.Models.Setup
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class EnabledToolsFilter: ToolMastersFilterItem
    {
        [JsonProperty("Unit")]
        public UnitEnum Unit { get; set; }
        [JsonProperty("ForSpindle")]
        public bool ForSpindle { get; set; }

        public EnabledToolsFilter()
        {
            ForSpindle = false;
        }
    }

    public class UnitToolsFilterValidator: AbstractValidator<UnitToolsFilter>
    {
        public UnitToolsFilterValidator()
        {
            RuleFor(x => x.UnitType)
                .IsInEnum()
                .WithErrorCode(ErrorCodesEnum.ERR_STP001.ToString());

            RuleFor(x => x.Unit)
                .IsInEnum()
                .WithErrorCode(ErrorCodesEnum.ERR_STP005.ToString());
        }
    }

    public class UnitToolsFilter
    {
        [JsonProperty("UnitType")]
        public PlantUnitEnum UnitType { get; set; }
        [JsonProperty("Unit")]
        public UnitEnum Unit { get; set; }
        [JsonProperty("ManualAction")]
        public bool ManualAction { get; set; }
        [JsonIgnore()]
        public MeasurementSystemEnum ConversionSystem { get; set; }

        public UnitToolsFilter()
        {
            Unit = UnitEnum.None;
            ManualAction = false;
        }
    }
}
