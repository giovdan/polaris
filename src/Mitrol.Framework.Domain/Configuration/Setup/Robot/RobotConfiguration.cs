namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class RobotConfiguration : NetworkDeviceConfiguration
    {
        internal const string s_typeJsonName = "Type";

        [JsonConstructor]
        public RobotConfiguration([JsonProperty(s_logJsonName)] bool? log,
                                  [JsonProperty(s_platformJsonName)] PlatformEnum? platform,
                                  [JsonProperty(s_ipAddressJsonName)] string ipAddress,
                                  [JsonProperty(s_ipPortJsonName)] ushort? ipPort,
                                  [JsonProperty(s_typeJsonName)] RobotTypeEnum? type)
            : base(log, platform, ipAddress, ipPort ?? 0)
        {
            Type = type;
        }

        [JsonProperty(s_typeJsonName)]
        public RobotTypeEnum? Type { get; protected set; }

        [JsonIgnore]
        public bool IsPresent => Type != RobotTypeEnum.None;
    }
}
