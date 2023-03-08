namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class DrillUnitConfiguration : UnitConfiguration, IUnitConfiguration
    {
        internal const string s_slotCountJsonName = "SlotCount";
        internal const string s_orientJsonName = "Orient";
        internal const string s_linearToolChangeTypeJsonName = "LinearToolChangeType";
        internal const string s_manualToolChangeSlotIndexJsonName = "ManualToolChangeSlotIndex";
        internal const string s_wheelToolChangeOffsetJsonName = "WheelToolChangeOffset";

        public DrillUnitConfiguration() { }

        [JsonConstructor]
        public DrillUnitConfiguration([JsonProperty(s_unitJsonName)] UnitEnum? unit,
                                      [JsonProperty(s_gridPositionJsonName)] GridPosition gridPosition,
                                      [JsonProperty(s_slotCountJsonName)] int? slotCount,
                                      [JsonProperty(s_orientJsonName)] bool? orient,
                                      [JsonProperty(s_linearToolChangeTypeJsonName)] byte? linearToolChangeType,
                                      [JsonProperty(s_manualToolChangeSlotIndexJsonName)] byte? manualToolChangeSlotIndex,
                                      [JsonProperty(s_wheelToolChangeOffsetJsonName)] short? wheelToolChangeOffset)
            : base(unit, gridPosition)
        {
            SlotCount = slotCount;
            Orient = orient;
            LinearToolChangeType = linearToolChangeType;
            ManualToolChangeSlotIndex = manualToolChangeSlotIndex;
            WheelToolChangeOffset = wheelToolChangeOffset;
        }

        // SlotCount > 0 == is present
        [JsonProperty(s_slotCountJsonName)]
        public int? SlotCount { get; protected set; }

        [JsonProperty(s_orientJsonName)]
        public bool? Orient { get; protected set; }

        [JsonProperty(s_linearToolChangeTypeJsonName)]
        public byte? LinearToolChangeType { get; protected set; }
        
        [JsonProperty(s_manualToolChangeSlotIndexJsonName)]
        public byte? ManualToolChangeSlotIndex { get; protected set; }

        [JsonProperty(s_wheelToolChangeOffsetJsonName)]
        public short? WheelToolChangeOffset { get; protected set; }

        [JsonIgnore]
        public override bool IsPresent => SlotCount > 0;
    }
}