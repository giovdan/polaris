namespace Mitrol.Framework.Domain.Production.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class StockItem : IEntityWithAttributeValues
    {
        public StockItem()
        {

        }

        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Attributes")]
        public Dictionary<DatabaseDisplayNameEnum, object> Attributes { get; set; }

        /// <summary>
        /// Quantità totali (QT)
        /// </summary>
        [JsonProperty("TotalQuantity")]
        public int TotalQuantity { get; set; }

        /// <summary>
        /// Quantità eseguite (QF)
        /// </summary>
        [JsonProperty("ExecutedQuantity")]
        public int ExecutedQuantity { get; set; }

        /// <summary>
        /// Tipo di profilo
        /// </summary>
        [JsonProperty("ProfileType")]
        public ProfileTypeEnum ProfileType { get; set; }

        [JsonProperty("Material")]
        public MaterialItem Material { get; set; }
    }
}
