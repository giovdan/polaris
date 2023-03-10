namespace Mitrol.Framework.Domain.Bus.Events
{
    using Mitrol.Framework.Domain.Bus;
    using Mitrol.Framework.Domain.Bus.Enums;
    using Newtonsoft.Json;

    public class CommandEvent : Event
    {
        [JsonProperty(PropertyName = "EventCode")]
        public CommandEnum EventCode { get; set; }
        [JsonProperty("CommandId")]
        public string CommandId { get; set; }
        [JsonProperty("Status")]
        public GenericEventStatusEnum Status { get; set; }
        [JsonProperty("Message")]
        public string Message { get; set; }
    }
}
