namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class BaseStockItem: IEntityWithImage
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Identifiers")]
        public Dictionary<string, string> Identifiers { get; set; }

        [JsonProperty("UMLocalizationKey")]
        public string UMLocalizationKey { get; set; }

        [JsonProperty("MaterialCode")]
        public string MaterialCode { get; set; }

        [JsonProperty("ImageCode")]
        public string ImageCode { get; set; }

        [JsonProperty("SuggestedFormat")]
        public string SuggestedFormat { get; set; }

        [JsonProperty("ProfileType")]
        public ProfileTypeEnum ProfileType { get; set; }

        [JsonProperty("StockType")]
        public StockTypeEnum StockType { get; set; }
    }
}
