using Mitrol.Framework.Domain.Enums;
using Newtonsoft.Json;

namespace Mitrol.Framework.Domain.Macro
{
    /// <summary>
    /// Classe per filtrare i dati di configurazione delle macro
    /// </summary>
    public class MacroConfigurationFilter
    {
        /// <summary>
        /// Tipo di Macro per si vuole ottenere la lista delle macro disponibili
        /// </summary>
        [JsonProperty("MacroType")]
        public MacroTypeEnum MacroType { get; set; }

        /// <summary>
        /// Tipo di profilo 
        /// </summary>
        [JsonProperty("ProfileType")]
        public ProfileTypeEnum ProfileType { get; set; }

        /// <summary>
        /// Nome macro
        /// </summary>
        [JsonProperty("MacroName")]
        public string MacroName { get; set; }
    }
}
