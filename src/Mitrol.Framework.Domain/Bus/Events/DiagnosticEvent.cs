namespace Mitrol.Framework.Domain.Bus.Events
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class DiagnosticEvent : Event
    {
        [JsonProperty(PropertyName = "EventContext")]
        public EventContextEnum EventContext { get; set; }

        [JsonProperty("MachineName")]
        public string MachineName { get; set; }

        [JsonProperty("Log")]
        public string Log { get; set; }
    }
}