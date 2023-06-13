namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Newtonsoft.Json;
    using System;

    public class MachineParameterItem : IConvertable, IEntityWithImage
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Value")]
        public decimal Value { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("ImageCode")]
        public string ImageCode { get; set; }

        [JsonProperty("DescriptionCode")]
        public string DescriptionCode { get; set; }

        [JsonProperty("MaximumValue")]
        public decimal MaximumValue { get; set; }

        [JsonProperty("MinimumValue")]
        public decimal MinimumValue { get; set; }

        [JsonProperty("DataFormat")]
        public AttributeDataFormatEnum ItemDataFormat { get; set; }

        [JsonProperty("HelpDescriptionCode")]
        public string HelpDescriptionCode { get; set; }

        [JsonProperty("IconCode")]
        public string IconCode { get; set; }

        [JsonProperty("Category")]
        public ParameterCategoryEnum Category { get; set; }

        [JsonProperty("UMLocalizationKey")]
        public string UMLocalizationKey { get; set; }

        [JsonProperty("DecimalPrecision")]
        public int DecimalPrecision { get; set; }

        [JsonProperty("UpdatedOn")]
        public DateTime? UpdatedOn { get; set; }

        [JsonProperty("NumberOfUpdates")]
        public int NumberOfUpdates { get; set; }

        [JsonProperty("ProtectionLevel")]
        public string ProtectionLevel { get; set; }
    }
}
