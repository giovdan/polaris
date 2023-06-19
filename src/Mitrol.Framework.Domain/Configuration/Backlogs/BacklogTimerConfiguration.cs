namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;

    public class BacklogTimerConfiguration
    {
        internal const string s_indexJsonName = "Index";
        internal const string s_localizationKeyJsonName = "LocalizationKey";

        public BacklogTimerConfiguration()
        {
        }

        [JsonConstructor]
        public BacklogTimerConfiguration([JsonProperty(s_indexJsonName)] int index,
                                  [JsonProperty(s_localizationKeyJsonName)] string localizationKey)
        {
            Index = index;
            LocalizationKey = localizationKey;
        }

        [JsonProperty(s_indexJsonName)]
        public int Index { get; protected set; }

        [JsonProperty(s_localizationKeyJsonName)]
        public string LocalizationKey { get; protected set; }
    }
}