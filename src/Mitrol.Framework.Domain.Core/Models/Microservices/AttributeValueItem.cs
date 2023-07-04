namespace Mitrol.Framework.Domain.Models
{
    using Amazon.DynamoDBv2.Model;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models.Core;
    using Newtonsoft.Json;

    public class BaseAttributeValueItem
    {
        public BaseAttributeValueItem(AttributeKindEnum attributeKindId, object value)
        {
            AttributeKindId = attributeKindId;
            CurrentValue = value;
        }

        public object CurrentValue { get; set; }
        public AttributeKindEnum AttributeKindId { get; }
    }

    public class AttributeValueItem
    {
        [JsonProperty("Source")]
        public object Source { get; set; }
        [JsonProperty("CurrentValue")]
        public object CurrentValue { get; set; }
        [JsonProperty("RealValue")]
        public object RealValue { get; set; }
        [JsonProperty("CurrentValueId")]
        public long CurrentValueId { get; set; }
        [JsonProperty("ValueType")]
        public ValueTypeEnum ValueType { get; set; }
        [JsonProperty("CurrentOverrideValue")]
        public AttributeOverrideValueItem CurrentOverrideValue { get; set; }
        [JsonProperty("MinimumValue")]
        public decimal? MinimumValue { get; set; }
        [JsonProperty("MaximumValue")]
        public decimal? MaximumValue { get; set; }

        public AttributeValueItem()
        {
            Source = null;
            ValueType = ValueTypeEnum.Flat;
            CurrentOverrideValue = new AttributeOverrideValueItem();
        }
    }

    public class AttributeOverrideValueItem
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("OverrideType")]
        public OverrideTypeEnum OverrideType { get; set; }
        [JsonProperty("Value")]
        public decimal Value { get; set; }

        public AttributeOverrideValueItem()
        {
            OverrideType = OverrideTypeEnum.None;
        }

        public AttributeOverrideValueItem(OverrideTypeEnum overrideType)
        {
            OverrideType = overrideType;
        }
    }

}
