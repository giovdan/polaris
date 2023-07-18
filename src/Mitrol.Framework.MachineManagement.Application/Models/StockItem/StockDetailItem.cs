namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class StockDetailItem
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("ProfileType")]
        public string ProfileType { get; set; }

        [JsonProperty("ProfileTypeId")]
        public long ProfileTypeId { get; set; }

        [JsonProperty("MaterialTypeId")]
        public MaterialTypeEnum MaterialTypeId { get; set; } 

        [JsonProperty("Groups")]
        public IEnumerable<BaseGroupItem<AttributeDetailItem>> Groups { get; set; }

        [JsonProperty("ProfileAttributes")]
        public IEnumerable<AttributeDetailItem> ProfileAttributes { get; set; }

        public StockDetailItem()
        {
            Groups = Array.Empty<BaseGroupItem<AttributeDetailItem>>();
            ProfileAttributes = Array.Empty<AttributeDetailItem>();
            ProfileType = string.Empty;
        }
    }
}
