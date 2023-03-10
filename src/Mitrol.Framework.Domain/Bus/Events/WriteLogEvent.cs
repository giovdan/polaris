namespace Mitrol.Framework.Domain.Bus.Events
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class WriteLogEvent : Event
    {
        public WriteLogEvent()
        {
        }

        [JsonProperty(PropertyName = "EventType")]
        public EventTypeEnum EventType { get; set; }

        [JsonProperty(PropertyName = "EventContext")]
        public EventContextEnum EventContext { get; set; }

        [JsonProperty(PropertyName = "EventCode")]
        public string EventCode => $"{EventType}_{EventContext}";

        [JsonProperty(PropertyName = "Method")]
        public string Method { get; set; }

        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "EventInput")]
        public string EventInput { get; set; }

        [JsonProperty(PropertyName = "EventOutput")]
        public string EventOutput { get; set; }

        [JsonProperty("LoggedUser")]
        public string LoggedUser { get; set; }

        [JsonProperty("MachineName")]
        public string MachineName { get; set; }

        [JsonProperty("SessionId")]
        public string SessionId { get; set; }
    }
}