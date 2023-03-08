namespace Mitrol.Framework.Domain.Configuration
{
    using FluentValidation;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class that represents the settings for Cnc configuration.
    /// </summary>
    public class CncConfiguration : PlatformConfiguration
    {
        internal const string s_axesJsonName = "Axes";
        internal const string s_axisGroupsJsonName = "AxisGroups";
        internal const string s_etherCATJsonName = "EtherCAT";
        internal const string s_fanucJsonProperty = "Fanuc";
        internal const string s_fastCycleTimeJsonName = "FastCycleTime";
        internal const string s_inOutCycleTimeJsonName = "InOutCycleTime";
        internal const string s_mitComJsonProperty = "Mitrol";
        internal const string s_axesOverrideJsonProperty = "AxesOverride";
        internal const string s_spindlesOverrideJsonProperty = "SpindlesOverride";

        /// <summary>
        /// Default constructor required for AutoMapper mappings.
        /// </summary>
        public CncConfiguration()
        { }

        /// <summary>
        /// Json constructor that allows de-serialization of read-only properties.
        /// </summary>
        [JsonConstructor]
        public CncConfiguration([JsonProperty(s_logJsonName)] bool? log,
                                [JsonProperty(s_platformJsonName)] PlatformEnum? platform,
                                [JsonProperty(s_inOutCycleTimeJsonName)] InOutCycleTimeEnum? inOutCycleTime,
                                [JsonProperty(s_fastCycleTimeJsonName)] FastCycleTimeEnum? fastCycleTime,
                                [JsonProperty(s_axisGroupsJsonName)] IReadOnlyList<AxisGroupConfiguration> axisGroups,
                                [JsonProperty(s_axesJsonName)] IReadOnlyList<AxisConfiguration> axes,
                                [JsonProperty(s_axesOverrideJsonProperty)] OverrideConfiguration axesOverride,
                                [JsonProperty(s_spindlesOverrideJsonProperty)] OverrideConfiguration spindlesOverride,
                                [JsonProperty(s_mitComJsonProperty)] MitrolConfiguration mitrol,
                                [JsonProperty(s_fanucJsonProperty)] FanucConfiguration fanuc,
                                [JsonProperty(s_etherCATJsonName)] EtherCATConfiguration etherCAT)
            : base(log, platform)
        {
            Mitrol = mitrol;
            Fanuc = fanuc;
            InOutCycleTime = inOutCycleTime;
            FastCycleTime = fastCycleTime;
            AxisGroups = axisGroups?
                .OrderBy(group => group.Order)?
                .Select((group, index) => { group.Id = index; return group; })
                .ToList();

            Axes = axes;
            AxesOverride = axesOverride;
            SpindlesOverride = spindlesOverride;
            EtherCAT = etherCAT;
        }

        [JsonIgnore]
        public bool AnyAxisOnSecondChannel => Axes?.Any(axis => axis.Path == 2) ?? false;

        [JsonIgnore]
        public bool AnyAxisOnThirdChannel => Axes?.Any(axis => axis.Path == 3) ?? false;

        [JsonIgnore]
        public bool AnyKnownAxisOnSecondChannel => Axes?.Any(axis => axis.Path == 2 && axis.KnownName != KnownAxisNameEnum.Unknown) ?? false;

        [JsonIgnore]
        public bool AuxillaryAxesOptimization => Axes?.Any(a => a.KnownName is KnownAxisNameEnum.U || a.KnownName is KnownAxisNameEnum.U2) ?? false;

        [JsonProperty(s_axesJsonName)]
        public IReadOnlyList<AxisConfiguration> Axes { get; protected set; }
        
        [JsonProperty(s_axisGroupsJsonName)]
        public IReadOnlyList<AxisGroupConfiguration> AxisGroups { get; protected set; }

        [JsonProperty(s_etherCATJsonName)]
        public EtherCATConfiguration EtherCAT { get; protected set; }

        [JsonProperty(s_fanucJsonProperty)]
        public FanucConfiguration Fanuc { get; protected set; }

        [JsonProperty(s_fastCycleTimeJsonName)]
        public FastCycleTimeEnum? FastCycleTime { get; protected set; }

        [JsonProperty(s_inOutCycleTimeJsonName)]
        public InOutCycleTimeEnum? InOutCycleTime { get; protected set; }

        [JsonIgnore]
        public IReadOnlyList<AxisConfiguration> KnownAxes => Axes?.Where(axis => axis.KnownName != KnownAxisNameEnum.Unknown).ToList();

        [JsonProperty(s_mitComJsonProperty)]
        public MitrolConfiguration Mitrol { get; protected set; }

        [JsonProperty(s_axesOverrideJsonProperty)]
        public OverrideConfiguration AxesOverride { get; protected set; }

        [JsonProperty(s_spindlesOverrideJsonProperty)]
        public OverrideConfiguration SpindlesOverride { get; protected set; }

        public OverrideConfiguration GetAxesOverrideConfig()
            => AxesOverride ?? new OverrideConfiguration(@readonly: false, steps: Enumerable.Range(start: 0, count: 11).Select(i => i * 10).ToArray());

        public OverrideConfiguration GetSpindlesOverrideConfig()
            => SpindlesOverride ?? new OverrideConfiguration(@readonly: false, steps: Enumerable.Range(start: 5, count: 8).Select(i => i * 10).ToArray());
    }
}