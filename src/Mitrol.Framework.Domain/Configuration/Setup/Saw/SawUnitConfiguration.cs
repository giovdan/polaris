namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class SawUnitConfiguration : NetworkDeviceConfiguration, IUnitConfiguration
    {
        internal const string s_bladeTypeJsonName = "BladeType";
        internal const string s_forwardSpeedTypeJsonName = "BladeForwardSpeedType";
        internal const string s_typeJsonName = "Type";
        internal const string s_bladeReferenceOriginJsonName = "BladeReferenceOrigin";
        internal const string s_tailCutsEnabled = "TailCutsEnabled";

        public SawUnitConfiguration()
        { }

        [JsonConstructor]
        public SawUnitConfiguration([JsonProperty(s_logJsonName)] bool? log,
                                    [JsonProperty(s_platformJsonName)] PlatformEnum? platform,
                                    [JsonProperty(s_ipAddressJsonName)] string ipAddress,
                                    [JsonProperty(s_ipPortJsonName)] ushort? ipPort,
                                    [JsonProperty(UnitConfiguration.s_unitJsonName)] UnitEnum? unitId,
                                    [JsonProperty(UnitConfiguration.s_gridPositionJsonName)] GridPosition gridPosition,
                                    [JsonProperty(s_typeJsonName)] SawingMachineTypeEnum? type,
                                    [JsonProperty(s_bladeTypeJsonName)] BladeTypeEnum? bladeType,
                                    [JsonProperty(s_bladeReferenceOriginJsonName)] BladeReferenceOriginEnum? bladeReferenceOrigin,
                                    [JsonProperty(s_forwardSpeedTypeJsonName)] BladeForwardSpeedTypeEnum? bladeForwardSpeedType,
                                    [JsonProperty(s_tailCutsEnabled)] bool? tailCutsEnabled)
            : base(log, platform, ipAddress, ipPort)
        {
            Unit = unitId;
            GridPosition = gridPosition;
            Type = type;
            BladeType = bladeType;
            BladeForwardSpeedType = bladeForwardSpeedType;
            BladeReferenceOrigin = bladeReferenceOrigin;
        }

        [JsonIgnore]
        public UnitEnum Id => Unit ?? UnitEnum.None;

        [JsonProperty(UnitConfiguration.s_unitJsonName)]
        public UnitEnum? Unit { get; protected set; }

        [JsonProperty(UnitConfiguration.s_gridPositionJsonName)]
        public GridPosition GridPosition { get; protected set; }

        [JsonProperty(s_forwardSpeedTypeJsonName)]
        public BladeForwardSpeedTypeEnum? BladeForwardSpeedType { get; protected set; }

        [JsonProperty(s_bladeTypeJsonName)]
        public BladeTypeEnum? BladeType { get; protected set; }

        [JsonProperty(s_bladeReferenceOriginJsonName)]
        public BladeReferenceOriginEnum? BladeReferenceOrigin { get; protected set; }
        
        [JsonProperty(s_typeJsonName)]
        public SawingMachineTypeEnum? Type { get; protected set; }

        [JsonProperty(s_tailCutsEnabled)]
        public bool? TailCutsEnabled { get; protected set; }

        [JsonIgnore]
        public bool IsPresent => Type != null && Type != SawingMachineTypeEnum.None;
    }
}
