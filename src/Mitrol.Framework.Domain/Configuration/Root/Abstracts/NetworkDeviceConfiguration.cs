namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// Abstract class that represents a network device settings.
    /// </summary>
    public abstract class NetworkDeviceConfiguration : PlatformConfiguration
    {
        internal const string s_ipAddressJsonName = "IpAddress";
        internal const string s_ipPortJsonName = "IpPort";

        /// <summary>
        /// Default constructor required for AutoMapper mappings.
        /// </summary>
        public NetworkDeviceConfiguration()
        { }

        /// <summary>
        /// Json constructor that allows de-serialization of read-only properties.
        /// </summary>
        [JsonConstructor]
        public NetworkDeviceConfiguration([JsonProperty(s_logJsonName)] bool? log,
                                          [JsonProperty(s_platformJsonName)] PlatformEnum? platform,
                                          [JsonProperty(s_ipAddressJsonName)] string ipAddress,
                                          [JsonProperty(s_ipPortJsonName)] ushort? ipPort)
            : base(log, platform)
        {
            IpAddress = ipAddress;
            IpPort = ipPort;
        }

        [JsonProperty(s_ipAddressJsonName)]
        public string IpAddress { get; protected set; }

        [JsonProperty(s_ipPortJsonName)]
        public ushort? IpPort { get; protected set; }
    }
}