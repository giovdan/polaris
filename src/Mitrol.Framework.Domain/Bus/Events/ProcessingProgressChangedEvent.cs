namespace Mitrol.Framework.Domain.Bus.Events
{
    using Mitrol.Framework.Domain.Bus;
    using Newtonsoft.Json;

    public sealed class ProcessingProgressChangedEvent : Event
    {
        [JsonProperty("ProgressPercentage")]
        public int ProgressPercentage { get; set; }

        /// <summary>
        /// Remaining time in seconds.
        /// </summary>
        [JsonProperty("RemainingTime")]
        public int RemainingTime { get; set; }

        [JsonProperty("ProgressText")]
        public string ProgressText { get; set; }

        [JsonProperty("Status")]
        public GenericEventStatusEnum Status { get; set; }
    }
}
