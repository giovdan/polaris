namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;

    /// <summary>
    /// Abstract class that represents the log setting.
    /// </summary>
    public abstract class LogConfiguration
    {
        internal const string s_logJsonName = "Log";

        /// <summary>
        /// Default constructor required for AutoMapper mappings.
        /// </summary>
        public LogConfiguration()
        { }

        /// <summary>
        /// Json constructor that allows de-serialization of read-only properties.
        /// </summary>
        [JsonConstructor]
        public LogConfiguration([JsonProperty(s_logJsonName)] bool? log)
        {
            Log = log;
        }

        [JsonProperty(s_logJsonName)]
        public bool? Log { get; protected set; }
    }
}