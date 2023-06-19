namespace Mitrol.Framework.Domain.Production.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    public class FontConfigurationFilter
    {     
        /// <summary>
        /// Tipo di unità di marcatura
        /// </summary>
        [JsonProperty("MarkingUnit")]
        public MarkingUnitTypeEnum MarkingUnit { get; set; }

        /// <summary>
        /// Tipo di scribing (standard o a caratteri collegati)
        /// </summary>
        [JsonProperty("ScribingType")]
        public ScribingMarkingTypeEnum ScribingType { get; set; }

        /// <summary>
        /// Indice del font scelto
        /// </summary>
        [JsonProperty("FontIndex")]
        public int FontIndex { get; set; }
    }
}
