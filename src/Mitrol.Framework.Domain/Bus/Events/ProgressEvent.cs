namespace Mitrol.Framework.Domain.Bus.Events
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class ProgressEvent : Event
    {
        private const string s_messageJsonName = "Message";
        private const string s_eventTypeJsonName = "EventType";
        
        public ProgressEvent()
        {

        }

        [JsonConstructor]
        public ProgressEvent([JsonProperty(s_messageJsonName)] string message,
                             [JsonProperty(s_eventTypeJsonName)] EventTypeEnum eventType = EventTypeEnum.Information)
            : base()
        {
            EventType = eventType;
            Message = message;
        }

        [JsonProperty(s_eventTypeJsonName)]
        public EventTypeEnum EventType { get; protected set; }

        [JsonProperty(s_messageJsonName)]
        public string Message { get; protected set; }
    }
}