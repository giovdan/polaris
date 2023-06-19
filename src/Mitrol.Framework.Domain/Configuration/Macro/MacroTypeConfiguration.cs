namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;

    public class MacroTypeConfiguration
    {
        internal const string s_attributesConfigurationPathJsonName = "AttributesConfigurationFolder";
        internal const string s_attributesConfigurationFilenameJsonName = "AttributesConfigurationFilename";
        internal const string s_baseFolderJsonName = "BaseFolder";
        internal const string s_configurationFilenameJsonName = "ConfigurationFilename";
        internal const string s_assemblyFilenameJsonName = "AssemblyFilename";

        public MacroTypeConfiguration() { }

        [JsonConstructor]
        public MacroTypeConfiguration(
            [JsonProperty(s_attributesConfigurationPathJsonName)] string attributesConfigFolder,
            [JsonProperty(s_attributesConfigurationFilenameJsonName)] string attributesConfigurationFilename,
            [JsonProperty(s_baseFolderJsonName)] string baseFolder,
            [JsonProperty(s_configurationFilenameJsonName)] string configurationFilename,
            [JsonProperty(s_assemblyFilenameJsonName)] string assemblyFilename
        )
        {
            AttributesConfigurationFolder = attributesConfigFolder;
            AttributesConfigurationFilename = attributesConfigurationFilename;
            BaseFolder = baseFolder;
            ConfigurationFilename = configurationFilename;
            AssemblyFilename = assemblyFilename;
        }

        [JsonProperty(s_attributesConfigurationPathJsonName)]
        public string AttributesConfigurationFolder { get; protected set; }

        [JsonProperty(s_attributesConfigurationFilenameJsonName)]
        public string AttributesConfigurationFilename { get; protected set; }

        [JsonProperty(s_baseFolderJsonName)]
        public string BaseFolder { get; protected set; }

        [JsonProperty(s_configurationFilenameJsonName)]
        public string ConfigurationFilename { get; protected set; }

        [JsonProperty(s_assemblyFilenameJsonName)]
        public string AssemblyFilename { get; protected set; }
    }
}