namespace Mitrol.Framework.Domain.Bus
{
    using Newtonsoft.Json;

    public class GenericEventInfo
    {
        [JsonProperty("EventId")]
        public string EventId { get; set; }
        [JsonProperty("Status")]
        public GenericEventStatusEnum Status { get; set; }
    }
}
