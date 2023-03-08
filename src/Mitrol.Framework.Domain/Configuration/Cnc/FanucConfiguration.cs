namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Configuration.Models;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Extensions;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class FanucConfiguration : NetworkDeviceConfiguration
    {
        private const string s_drillAuxAxesOptimizationJsonName = "DrillAuxAxesOptimization";
        private const string s_gFunctionConfigurationJsonName = "GFunctions";
        private const string s_g270MaxJsonName = "G270_MAX";
        private const string s_g280TandemJsonName = "TandemModality";
        private const string s_compIsoCutJsonName = "IsoCutCompensation";
        private const string s_compIsoFreJsonName = "IsoMillCompensation";

        public FanucConfiguration() { }

        [JsonConstructor]
        public FanucConfiguration([JsonProperty(s_logJsonName)] bool? log,
                                  [JsonProperty(s_platformJsonName)] PlatformEnum? platform,
                                  [JsonProperty(s_ipAddressJsonName)] string ipAddress,
                                  [JsonProperty(s_ipPortJsonName)] ushort? ipPort,
                                  [JsonProperty(s_drillAuxAxesOptimizationJsonName)] bool? drillAuxAxesOptimization,
                                  [JsonProperty(s_gFunctionConfigurationJsonName)] FlagsDictionary<GFunctionEnum> gFunctions,
                                  [JsonProperty(s_g270MaxJsonName)] int g270MAX,
                                  [JsonProperty(s_g280TandemJsonName)] TandemEnum g280Tandem,
                                  [JsonProperty(s_compIsoCutJsonName)] bool compISOCut,
                                  [JsonProperty(s_compIsoFreJsonName)] bool compISOFre)
            : base(log, platform, ipAddress, ipPort)
        {
            DrillAuxAxesOptimization = drillAuxAxesOptimization ?? true;
            GFunctions = gFunctions;
            G270MAX = g270MAX;
            G280Tandem = g280Tandem;
            CompISOCut = compISOCut;
            CompISOFre = compISOFre;
        }

        [JsonProperty(s_drillAuxAxesOptimizationJsonName)]
        public bool DrillAuxAxesOptimization { get; protected set; } = true;

        [JsonProperty(s_gFunctionConfigurationJsonName)]
        public IReadOnlyDictionary<GFunctionEnum, bool> GFunctions { get; protected set; }

        /// <summary>
        /// Limite massimo del numero di fori da inviare al buffer DNC.
        /// </summary>
        [JsonProperty(s_g270MaxJsonName)]
        public int G270MAX { get; protected set; }

        [JsonProperty(s_g280TandemJsonName)]
        public TandemEnum G280Tandem { get; protected set; }

        [JsonProperty(s_compIsoCutJsonName)]
        public bool CompISOCut { get; protected set; }

        [JsonProperty(s_compIsoFreJsonName)]
        public bool CompISOFre { get; protected set; }

        [JsonIgnore]
        public bool IsPresent => IpAddress.IsNotNullOrEmpty();
    }
}