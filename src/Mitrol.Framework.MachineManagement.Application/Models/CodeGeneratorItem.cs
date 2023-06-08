namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Newtonsoft.Json;

    public class CodeGeneratorItem: IConvertable
    {
        [JsonIgnore]
        public string HashCode { get; set; }

        [JsonProperty("Value")]
        public object Value { get; set; }

        [JsonProperty("Order")]
        public int Order { get; set; }

        [JsonIgnore()]
        public AttributeDataFormatEnum ItemDataFormat { get; set; }
        public string UMLocalizationKey { get; set; }

        [JsonIgnore()]
        public int DecimalPrecision { get; set; }

        [JsonIgnore()]
        public AttributeKindEnum AttributeKind { get; set; }

        [JsonIgnore()]
        public string TypeName { get; set; }

        [JsonIgnore()]
        public string DisplayName { get; set; }

        [JsonIgnore()]
        public string InnerValue { get; set; }

    }
}