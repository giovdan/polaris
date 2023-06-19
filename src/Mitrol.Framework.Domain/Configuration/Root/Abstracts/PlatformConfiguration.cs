namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// Abstract class that represents the Platform setting.
    /// </summary>
    public abstract class PlatformConfiguration : LogConfiguration
    {
        internal const string s_platformJsonName = "Platform";

        /// <summary>
        /// Default constructor required for AutoMapper mappings.
        /// </summary>
        public PlatformConfiguration()
        { }

        /// <summary>
        /// Json constructor that allows de-serialization of read-only properties.
        /// </summary>
        [JsonConstructor]
        public PlatformConfiguration([JsonProperty(s_logJsonName)] bool? log,
                                     [JsonProperty(s_platformJsonName)] PlatformEnum? platform)
            : base(log)
        {
            Platform = platform;
        }

        /// <summary>
        /// Simulation setting.
        /// </summary>
        [JsonProperty(s_platformJsonName)]
        public PlatformEnum? Platform { get; protected set; }
    }
}