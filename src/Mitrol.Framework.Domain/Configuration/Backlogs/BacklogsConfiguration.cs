namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class BacklogsConfiguration
    {
        internal const string s_timeReferenceJsonName = "TimeReference";
        internal const string s_maxRecordsJsonName = "MaxRecords";
        internal const string s_timersJsonName = "Timers";

        public BacklogsConfiguration() { }

        [JsonConstructor]
        public BacklogsConfiguration([JsonProperty(s_timeReferenceJsonName)] int? timeRef,
                                     [JsonProperty(s_maxRecordsJsonName)] int? maxRecords,
                                     [JsonProperty(s_timersJsonName)] IReadOnlyList<BacklogTimerConfiguration> timers)
        {
            TimeRef = timeRef;
            MaxRecords = maxRecords;
            Timers = timers;
        }

        [JsonProperty(s_timeReferenceJsonName)]
        public int? TimeRef { get; protected set; }

        [JsonProperty(s_maxRecordsJsonName)]
        public int? MaxRecords { get; protected set; }

        [JsonProperty(s_timersJsonName)]
        public IReadOnlyList<BacklogTimerConfiguration> Timers { get; protected set; }
    }
}