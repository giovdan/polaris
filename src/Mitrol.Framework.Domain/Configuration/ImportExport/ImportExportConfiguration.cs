namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Class that represents the settings for Machine configuration.
    /// </summary>
    public class ImportExportConfiguration
    {
        internal const string s_foldersJsonName = "Folders";
        internal const string s_profileAttributeTolleranceThresholdJsonName = "ProfileDimensionsTollerance";

        /// <summary>
        /// Default constructor required for AutoMapper mappings.
        /// </summary>
        public ImportExportConfiguration()
        { }

        /// <summary>
        /// Json constructor that allows de-serialization of read-only properties.
        /// </summary>
        [JsonConstructor]
        public ImportExportConfiguration([JsonProperty(s_foldersJsonName)] string[] importExportFolders,
                                    [JsonProperty(s_profileAttributeTolleranceThresholdJsonName)] decimal profileAttributesTolleranceThreshold)
        {
            ProfileAttributesTolleranceThreshold = profileAttributesTolleranceThreshold;
            Folders = importExportFolders?.ToArray();
        }

        [JsonProperty(s_foldersJsonName)]
        public string[] Folders { get; protected set; }

        /// <summary>
        /// Tolleranza di confronto per import dei profili (dimensioni)
        /// </summary>
        [JsonProperty(s_profileAttributeTolleranceThresholdJsonName)]
        public decimal ProfileAttributesTolleranceThreshold { get; protected set; }
    }
}