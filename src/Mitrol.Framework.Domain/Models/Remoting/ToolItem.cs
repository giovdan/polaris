namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ToolItem : ToolItemIdentifiers, IEntityWithAttributeValuesAndIdentifiers
    {
        [JsonProperty("Attributes")]
        public Dictionary<DatabaseDisplayNameEnum, object> Attributes { get; set; }
    }
}
