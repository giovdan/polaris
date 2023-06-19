namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class DetailItem<T>
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set;  }

        [JsonProperty("Value")]
        public T Value { get; set; }

        [JsonProperty("AttributeDefinitionLinkId")]
        public long AttributeDefinitionLinkId { get; set; }

        [JsonIgnore()]
        public long EntityId { get; set; }

        [JsonIgnore()]
        public EntityTypeEnum EntityType { get; set; }

        [JsonProperty("EnumId")]
        public AttributeDefinitionEnum EnumId { get; set; }

        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }

        [JsonProperty("Order")]
        public int Order { get; set; }

        [JsonProperty("AttributeKind")]
        public AttributeKindEnum AttributeKind { get; set; }

        [JsonProperty("TypeName")]
        public string TypeName { get; set; }
    }
}