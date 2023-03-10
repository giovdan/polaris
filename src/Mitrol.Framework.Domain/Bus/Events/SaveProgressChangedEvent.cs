namespace Mitrol.Framework.Domain.Bus.Events
{
    using Mitrol.Framework.Domain.Bus;
    using Newtonsoft.Json;

    public sealed class SaveProgressChangedEvent: Event
    {
        [JsonProperty("Percentage")]
        public int Percentage { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("Status")]
        public GenericEventStatusEnum Status { get; set; }
    }
}
