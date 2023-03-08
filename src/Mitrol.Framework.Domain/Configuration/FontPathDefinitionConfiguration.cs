namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;

    public class FontPathDefinitionConfiguration
    {
        internal const string s_standardJsonName = "Standard";
        internal const string s_linkedCharactersJsonName = "LinkedCharacters";

        public FontPathDefinitionConfiguration() { }

        [JsonConstructor]
        public FontPathDefinitionConfiguration([JsonProperty(s_standardJsonName)] string standard,
                                  [JsonProperty(s_linkedCharactersJsonName)] string linkedCharacters)
        {
            Standard = standard;
            LinkedCharacters = linkedCharacters;
        }

        [JsonProperty(s_standardJsonName)]
        public string Standard { get; protected set; }

        [JsonProperty(s_linkedCharactersJsonName)]
        public string LinkedCharacters { get; protected set; }
    }
}