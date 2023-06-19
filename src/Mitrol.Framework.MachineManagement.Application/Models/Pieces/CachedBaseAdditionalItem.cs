namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class CachedBaseAdditionalItem
    {
        [JsonProperty("OperationType")]
        public OperationTypeEnum OperationType { get; set; }
        [JsonProperty("Attributes")]
        public IEnumerable<AttributeDetailItem> Attributes { get; set; }
        [JsonProperty("Index")]
        public int Index { get; set; }
    }

}
