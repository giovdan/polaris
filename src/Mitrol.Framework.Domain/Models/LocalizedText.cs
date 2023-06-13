namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class LocalizedText
    {
        public LocalizedText()
        {
            Arguments = new Dictionary<string, string>();
        }

        public LocalizedText(ProcessErrorEnum prcError)
        {
            LocalizationKey = prcError.ToString();
            Arguments = new Dictionary<string, string>
            {
                { "ErrorCode", $"ERR{(int)prcError}" }
            };
        }

        public LocalizedText(string localizationKey)
            : this()
        {
            LocalizationKey = localizationKey;
        }

        public LocalizedText(string localizationKey, Dictionary<string, string> arguments)
        {
            LocalizationKey = localizationKey;
            Arguments = arguments ?? throw new System.ArgumentNullException(nameof(arguments));
        }

        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }

        [JsonProperty("Arguments")]
        public Dictionary<string, string> Arguments { get; set; }
    }
}