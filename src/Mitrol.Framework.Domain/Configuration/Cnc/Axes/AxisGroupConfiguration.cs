namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class AxisGroupConfiguration
    {
        public AxisGroupConfiguration() { }

        [JsonConstructor]
        public AxisGroupConfiguration([JsonProperty("Order")] int? order,
                                     [JsonProperty("LocalizationKey")] string localizationKey,
                                     [JsonProperty("AxisNames")] List<string> axisNames)
        {
            Order = order;
            LocalizationKey = localizationKey;
            AxisNames = axisNames;
        }

        [JsonProperty("Id")]
        public int? Id { get; set; }

        [JsonProperty("Order")]
        public int? Order { get; protected set; }

        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; protected set; }

        [JsonProperty("AxisNames")]
        public IReadOnlyList<string> AxisNames { get; protected set; }
    }
}