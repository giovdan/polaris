namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class ReaJetConfiguration : NetworkDeviceConfiguration
    {
        internal const string s_inclinationJsonName = "Inclination";
        internal const string s_spacePlaceHolderJsonName = "SpacePlaceHolder";
        internal const string s_earlyPositioningJsonName = "EarlyPositioning";
        internal const string s_charSpacingJsonName = "CharSpacing";

        public ReaJetConfiguration() { }

        public ReaJetConfiguration([JsonProperty(s_logJsonName)] bool? log,
                                   [JsonProperty(s_platformJsonName)] PlatformEnum? platform,
                                   [JsonProperty(s_ipAddressJsonName)] string ipAddress,
                                   [JsonProperty(s_ipPortJsonName)] ushort? ipPort,
                                   [JsonProperty(s_inclinationJsonName)] float? inclination,
                                   [JsonProperty(s_spacePlaceHolderJsonName)] char? spacePlaceholder,
                                   [JsonProperty(s_earlyPositioningJsonName)] float? earlyPositioning,
                                   [JsonProperty(s_charSpacingJsonName)] float? charSpacing)
            : base(log, platform, ipAddress, ipPort ?? 0)
        {
            Inclination = inclination;
            SpacePlaceholder = spacePlaceholder;
            EarlyPositioning = earlyPositioning;
            CharSpacing = charSpacing;
        }

        [JsonProperty(s_inclinationJsonName)]
        public float? Inclination { get; protected set; }

        [JsonProperty(s_spacePlaceHolderJsonName)]
        public char? SpacePlaceholder { get; protected set; }

        [JsonProperty(s_earlyPositioningJsonName)]
        public float? EarlyPositioning { get; protected set; }

        [JsonProperty(s_charSpacingJsonName)]
        public float? CharSpacing { get; protected set; }
    }
}