
namespace Mitrol.Framework.Domain.Macro
{
    using Newtonsoft.Json;

    /// <summary>
    /// Configurazione del singolo attributo 
    /// </summary>
    public class AttributeConfiguration
    {
        /// <summary>
        /// Nome dell'attributo: enumerativo di tipo AttributeDefinitionEnum
        /// </summary>
        [JsonProperty("Parameter")]
        public string Parameter { get; set; }

        /// <summary>
        /// Localizzazione del parametro
        /// </summary>
        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }

        /// <summary>
        /// Eventuale nuovo formato dei dati 
        /// </summary>
        [JsonProperty("Format")]
        public string DataFormat { get; set; }

    }

    
}
