namespace Mitrol.Framework.Domain.Macro
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Macro.Enum;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System.Collections.Generic;
    /// <summary>
    /// COnfigurazione del gruppo 
    /// </summary>
    public class MacroCluster
    {
        /// <summary>
        /// Enumerativo corrispondente al tipo di profilo a cui si applica il gruppo di macro 
        /// </summary>
        [JsonProperty("ProfileType")]
        public ProfileTypeEnum ProfileType { get; set; } 

        /// <summary>
        /// Gruppo di appartenenza della macro (enumerativo corrispondente a 1="edge", 2="apexes", 3="internal", 4="length")
        /// </summary>
        [JsonProperty("Group")]
        [JsonConverter(typeof(StringEnumConverter))]
        public MacroGroupEnum Group { get; set; }

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

        public MacroCluster()
        {

        }
    }
}
