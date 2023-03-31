namespace Mitrol.Framework.Domain.Models
{
    using Newtonsoft.Json;

    public class PermissionItem
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("Code")]
        public string Code { get; set; }
        [JsonProperty("Value")]
        public bool Value { get; set; }
    }
}