namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class ToolItemFilter
    {
        public ToolItemFilter()
        {
            ConversionSystem = MeasurementSystemEnum.MetricSystem;
            ProcessingTechnology = ProcessingTechnologyEnum.Default;
        }

        [JsonProperty("ConversionSystem")]
        public MeasurementSystemEnum ConversionSystem { get; set; }
        [JsonProperty("ToolManagementId")]
        public long ToolManagementId { get; set; }
        [JsonProperty("ProcessingTechnology")]
        public ProcessingTechnologyEnum ProcessingTechnology { get; set; }
    }
}