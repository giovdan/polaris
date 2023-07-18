namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using Newtonsoft.Json;

    public class StockListItem: BaseStockItem
    {
 
        /// <summary>
        /// Quantità iniziali
        /// Qi
        /// </summary>
        [JsonProperty("TotalQuantity")]
        public int TotalQuantity { get; set; }

        /// <summary>
        /// Quantità eseguite
        /// Qe
        /// </summary>
        /// <returns></returns>
        [JsonProperty("ExecutedQuantity")]
        public int ExecutedQuantity { get; set; }

        public StockListItem Clone() => this.MemberwiseClone() as StockListItem;
    }
}
