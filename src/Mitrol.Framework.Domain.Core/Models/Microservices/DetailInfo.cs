namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using System;
    using Newtonsoft.Json;

    public class DetailInfo
    {
        public DetailInfo(string localizationKey, object value)
        {
            LocalizationKey = localizationKey ?? throw new ArgumentNullException(nameof(localizationKey));
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }

        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }
        [JsonProperty("Value")]
        public object Value { get; set; }


    }

}
