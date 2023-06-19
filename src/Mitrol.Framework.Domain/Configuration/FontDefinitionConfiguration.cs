namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class FontDefinitionConfiguration
    {
        internal const string s_definitionsFilenameJsonName = "FontsDefinitionFileName";
        internal const string s_fontsJsonName = "Fonts";
        internal const string s_pathsJsonName = "Paths";

        public FontDefinitionConfiguration()
        { }

        [JsonConstructor]
        public FontDefinitionConfiguration([JsonProperty(s_fontsJsonName)] IReadOnlyList<string> fonts,
                                           [JsonProperty(s_definitionsFilenameJsonName)] string fontsDefinitionFileName,
                                           [JsonProperty(s_pathsJsonName)] FontPathDefinitionConfiguration paths)
        {
            Fonts = fonts;
            FontsDefinitionFileName = fontsDefinitionFileName;
            Paths = paths;
        }

        [JsonProperty(s_fontsJsonName)]
        public IReadOnlyList<string> Fonts { get; protected set; }

        [JsonProperty(s_definitionsFilenameJsonName)]
        public string FontsDefinitionFileName { get; protected set; }

        [JsonProperty(s_pathsJsonName)]
        public FontPathDefinitionConfiguration Paths { get; protected set; }
    }
}