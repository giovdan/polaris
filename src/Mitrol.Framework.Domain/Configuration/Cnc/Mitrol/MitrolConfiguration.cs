namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class MitrolConfiguration : NetworkDeviceConfiguration
    {
        public MitrolConfiguration() { }
        
        [JsonConstructor]
        public MitrolConfiguration([JsonProperty(s_logJsonName)] bool? log,
                                   [JsonProperty(s_platformJsonName)] PlatformEnum? platform,
                                   [JsonProperty(s_ipAddressJsonName)] string ipAddress,
                                   [JsonProperty(s_ipPortJsonName)] ushort? ipPort)
            : base(log, platform, ipAddress, ipPort)
        {
        }
    }
}