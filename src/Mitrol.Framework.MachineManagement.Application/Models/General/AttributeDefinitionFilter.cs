
namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class AttributeDefinitionFilter
    {
        [JsonProperty("ToolType")]
        public ToolTypeEnum ToolType { get; set; }
        [JsonIgnore()]
        public AttributeTypeEnum AttributeType { get; set; }
        [JsonIgnore()]
        public Dictionary<DatabaseDisplayNameEnum, object> Values { get; set; }
        [JsonIgnore()]
        public MeasurementSystemEnum ConversionSystem { get; set; }

        public AttributeDefinitionFilter()
        {
            AttributeType = AttributeTypeEnum.All;
            Values = new Dictionary<DatabaseDisplayNameEnum, object>();
            ConversionSystem = MeasurementSystemEnum.MetricSystem;
        }
    }
}
