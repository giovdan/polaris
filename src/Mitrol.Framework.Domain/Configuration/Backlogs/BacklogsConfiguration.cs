namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class BacklogsConfiguration
    {
        internal const string s_timeReferenceJsonName = "TimeReference";
        internal const string s_maxRecordsJsonName = "MaxRecords";
        internal const string s_timersJsonName = "Timers";
        internal const string s_consumptionReferenceJsonName = "ConsumptionReference";
        internal const string s_consumeJsonName = "Consume";

        public BacklogsConfiguration() { }

        [JsonConstructor]
        public BacklogsConfiguration([JsonProperty(s_timeReferenceJsonName)] int? timeRef,
                                     [JsonProperty(s_maxRecordsJsonName)] int? maxRecords,
                                     [JsonProperty(s_timersJsonName)] IReadOnlyList<BacklogTimerConfiguration> timers,
                                     [JsonProperty(s_consumptionReferenceJsonName)] int? consumptionRef,
                                     [JsonProperty(s_consumeJsonName)] IReadOnlyList<BacklogTimerConfiguration> consume)
        {
            TimeRef = timeRef;
            MaxRecords = maxRecords;
            Timers = timers;
            Consume = consume;
            ConsumptionRef = consumptionRef;
        }

        [JsonProperty(s_timeReferenceJsonName)]
        public int? TimeRef { get; protected set; }

        [JsonProperty(s_maxRecordsJsonName)]
        public int? MaxRecords { get; protected set; }

        [JsonProperty(s_timersJsonName)]
        public IReadOnlyList<BacklogTimerConfiguration> Timers { get; protected set; }

        [JsonProperty(s_consumptionReferenceJsonName)]
        public int? ConsumptionRef { get; protected set; }

        [JsonProperty(s_consumeJsonName)]
        public IReadOnlyList<BacklogTimerConfiguration> Consume { get; protected set; }
    }
}