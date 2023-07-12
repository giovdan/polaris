namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    
    public class ToolItemIdentifiersFilter
    {
        public ToolItemIdentifiersFilter()
        {
            ConversionSystem = MeasurementSystemEnum.MetricSystem;
            UnitType = PlantUnitEnum.All;
            ToolUnitMask = ToolUnitMaskEnum.All;
        }
        [JsonIgnore()]
        public MeasurementSystemEnum ConversionSystem { get; set; }
        [JsonProperty("UnitType")]
        public PlantUnitEnum UnitType { get; set; }
        [JsonProperty("ToolUnitMask")]
        public ToolUnitMaskEnum ToolUnitMask { get; set; }
    }
}