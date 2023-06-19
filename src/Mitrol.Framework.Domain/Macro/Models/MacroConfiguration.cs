namespace Mitrol.Framework.Domain.Macro
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Macro.Enum;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    /// <summary>
    /// Configurazione di una singola Macro
    /// </summary>
    public class MacroConfiguration
    {
        
        /// <summary>
        /// Nome della macro
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }


        /// <summary>
        /// Booleano che indica se la macro è abilitata 
        /// </summary>
        [JsonProperty("Enable")]
        public bool Enable { get; set; }

        /// <summary>
        /// Nome dell'immagine che rappresenta graficamente la macro 
        /// </summary>
        [JsonProperty("ImageCode")]
        public string ImageCode { get; set; }

        /// <summary>
        ///  Lato della lavorazione (Enumerativo corrispondente a "A", "B", "C")
        /// </summary>
        [JsonProperty("Side")]
        public OperationSideEnum Side { get; set; }

        /// <summary>
        /// Vertice X della lavorazione (Enumerativo corrispondente "Initial", "Final", "Undefined")
        /// </summary>
        [JsonProperty("Vertex_X")]
        public LongitudinalVertexPositionEnum Vertex_X { get; set; }

        /// <summary>
        /// Vertice Y della lavorazione (Enumerativo corrispondente "Top", "Bottom", "Undefined")
        /// </summary>
        [JsonProperty("Vertex_Y")]
        public TransverseVertexPositionEnum Vertex_Y { get; set; }


        /// <summary>
        /// Lista degli attributi della macro
        /// </summary>
        [JsonProperty("Attributes")]
        public List<AttributeConfiguration> Attributes { get; set; }

        /// <summary>
        /// Lista delle tecnologie abilitate
        /// </summary>
        [JsonProperty("Groups")]
        public List<GroupConfiguration> Technologies { get; set; }

    }
}
