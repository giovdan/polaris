namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;

    public class ConsoleConfiguration
    {
        internal const string s_titleLocalizationKeyJsonName = "TitleLocalizationKey";
        internal const string s_urlJsonName = "Url";

        [JsonConstructor]
        public ConsoleConfiguration([JsonProperty(s_titleLocalizationKeyJsonName)] string titleLocalizationKey,
                                    [JsonProperty(s_urlJsonName)] string url)
        {
            TitleLocalizationKey = titleLocalizationKey;
            Url = url;
        }

        [JsonProperty(s_titleLocalizationKeyJsonName)]
        public string TitleLocalizationKey { get; protected set; }

        [JsonProperty(s_urlJsonName)]
        public string Url { get; protected set; }
    }
}