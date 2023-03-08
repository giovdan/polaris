namespace Mitrol.Framework.Domain.Macro
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    /// <summary>
    /// Classe relativa alla configurazione dei gruppi Tecnologici 
    /// </summary>
    public class GroupConfiguration
    {
        /// <summary>
        /// Nome della tecnologia  abilitata
        /// </summary>
        [JsonProperty("GroupName")]
        public string GroupName { get; set; }

        /// <summary>
        /// Lista di stringhe corrispondenti agli enumerativi del ToolTypeEnum abilitati per questa tecnologia
        /// </summary>
        [JsonProperty("Values")]
        public Dictionary<string,List<string>> Values { get; set; }

    }
}
