namespace Mitrol.Framework.Domain.Production.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    // Material item usato per lettura o aggiornamento
    public class MaterialItem : IEntityWithAttributeValues
    {
        [JsonProperty("Id")]
        public long Id { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("Attributes")]
        public Dictionary<DatabaseDisplayNameEnum, object> Attributes { get; set; }

        public MaterialItem()
        {
        }
    }
}
