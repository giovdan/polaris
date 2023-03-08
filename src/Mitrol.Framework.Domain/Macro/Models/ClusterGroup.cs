namespace Mitrol.Framework.Domain.Macro
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    /// <summary>
    /// COnfigurazione del gruppo 
    /// </summary>
    public class ClusterGroup
    {
        /// <summary>
        /// Enumerativo corrispondente al tipo di profilo a cui si applica il gruppo di macro 
        /// </summary>
        [JsonProperty("ProfileType")]
        public string ProfileType { get; set; } 

        /// <summary>
        /// Gruppo di appartenenza della macro (enumerativo corrispondente a 1="edge", 2="apexes", 3="internal", 4="length")
        /// </summary>
        [JsonProperty("Cluster")]
        public string Cluster { get; set; }

        /// <summary>
        /// Localizzazione del cluster
        /// </summary>
        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }

        /// <summary>
        /// Lista delle configurazioni delle macro nel cluster indicato
        /// </summary>
        [JsonProperty("Macros")]
        public List<MacroConfiguration> Macros { get; set; }
    }
}
