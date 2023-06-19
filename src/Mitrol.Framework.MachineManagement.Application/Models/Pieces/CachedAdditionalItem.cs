namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class CachedAdditionalItem: CachedBaseAdditionalItem
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonIgnore()]
        public long PieceId { get; set; }
        [JsonProperty("CodeGenerators")]
        public Dictionary<DatabaseDisplayNameEnum, object> CodeGenerators { get; internal set; }
        [JsonProperty("SuggestedFormat")]
        public string SuggestedFormat { get; internal set; }
        [JsonProperty("Status")]
        public OperationRowStatusEnum Status { get; set; }
        [JsonProperty("Icons")]
        public IEnumerable<string> Icons { get; set; }
    }
}
