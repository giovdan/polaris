namespace Mitrol.Framework.Domain.Production.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// classe di oggetti macro per comunicazione tra FE e BE
    /// </summary>
    public class MacroInfoItem
    {
        /// <summary>
        /// Nome dell'immagine
        /// </summary>
        [JsonProperty("Image")]
        public string Image { get; set; }

        /// <summary>
        /// Nome della macro
        /// </summary>
        [JsonProperty("MacroName")]
        public string MacroName { get; set; }

        /// <summary>
        /// Tipo di operazione (enum corrispondente a macro di taglio, macro di fresatura e macro robot) 
        /// </summary>
        [JsonProperty("Type")]
        public OperationTypeEnum Type { get; set; }
    }
}
