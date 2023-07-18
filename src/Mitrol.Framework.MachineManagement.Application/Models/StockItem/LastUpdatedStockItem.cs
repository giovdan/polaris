namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using Newtonsoft.Json;

    public class LastUpdatedStockItem: BaseStockItem
    {
        [JsonProperty("HeatNumber")]
        public string HeatNumber { get; set; }

        [JsonProperty("Supplier")]
        public string Supplier { get; set; }
    }
}
