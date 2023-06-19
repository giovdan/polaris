namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Define ToolRange Type Enum
    /// </summary>
    [Flags]
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum ToolRangeTypeEnum : int
    {
        None = 0,
        [EnumSerializationName("DRILL")]
        [DatabaseDisplayName("DRILL")]
        [Description("Tabelle di Foratura")]
        [RelatedFilter(DatabaseDisplayNameEnum.MaterialType, isMainFilter: true, priority: 1)]
        [RelatedFilter(DatabaseDisplayNameEnum.ToolWorkType, isMainFilter: true, priority: 2)]
        [RelatedFilter(DatabaseDisplayNameEnum.MinThickness, priority: 3)]
        [RelatedFilter(DatabaseDisplayNameEnum.MaxThickness, priority: 4)]
        [SuggestedFormat("${ToolWorkType}")]
        [SuggestedFormat("${MinThickness} - ${MaxThickness}", level: 2)]
        Drill = 1,
        [EnumSerializationName("CUT")]
        [DatabaseDisplayName("CUT")]
        [Description("Tabelle di Taglio")]
        [RelatedFilter(DatabaseDisplayNameEnum.MaterialType, isMainFilter: true, priority: 1)]
        [RelatedFilter(DatabaseDisplayNameEnum.MinThickness, priority: 2)]
        [RelatedFilter(DatabaseDisplayNameEnum.MaxThickness, priority: 3)]
        [RelatedFilter(DatabaseDisplayNameEnum.MinRequiredConsole, priority: 4)]
        [RelatedFilter(DatabaseDisplayNameEnum.PlasmaGas, priority: 5)]
        [RelatedFilter(DatabaseDisplayNameEnum.ShieldGas, priority: 5)]
        [SuggestedFormat("${MinThickness} - ${MaxThickness} - ${MinRequiredConsole} - ${PlasmaGas} - ${ShieldGas} {HasBevelRows} {HasTrueHoleRows}")]
        Cut = 2,
        [EnumSerializationName("MARK")]
        [DatabaseDisplayName("MARK")]
        [Description("Tabelle di Marcatura")]
        [RelatedFilter(DatabaseDisplayNameEnum.MaterialType, isMainFilter: true, priority: 1)]
        [RelatedFilter(DatabaseDisplayNameEnum.PlasmaGas, priority: 2)]
        [SuggestedFormat("${PlasmaGas}")]
        [IsSubRange(SubRangeTypeEnum.Mark)]
        Mark = 4,
        [EnumSerializationName("TRUEHOLE")]
        [DatabaseDisplayName("TRUEHOLE")]
        [Description("Tabelle di TrueHole")]
        [RelatedFilter(DatabaseDisplayNameEnum.TrueHoleAreaMin, priority: 1)]
        [RelatedFilter(DatabaseDisplayNameEnum.TrueHoleAreaMax, priority: 2)]
        [SuggestedFormat("${TrueHoleAreaMin} - ${TrueHoleAreaMax}")]
        [IsSubRange(SubRangeTypeEnum.TrueHole)]
        TrueHole = 8,
        [EnumSerializationName("BEVEL")]
        [DatabaseDisplayName("BEVEL")]
        [Description("Tabelle di Taglio Bevel")]
        [RelatedFilter(DatabaseDisplayNameEnum.BevelType, isMainFilter: true, priority: 1)]
        [RelatedFilter(DatabaseDisplayNameEnum.MinBevelAngle, priority: 2)]
        [RelatedFilter(DatabaseDisplayNameEnum.MaxBevelAngle, priority: 3)]
        [RelatedFilter(DatabaseDisplayNameEnum.MinBevelLand, priority: 4)]
        [RelatedFilter(DatabaseDisplayNameEnum.MaxBevelLand, priority: 5)]
        [SuggestedFormat("${BevelType} {MinBevelAngle} - ${MaxBevelAngle} ${MinBevelLand} - ${MaxBevelLand}")]
        [IsSubRange(SubRangeTypeEnum.Bevel)]
        Bevel = 16,
        [EnumSerializationName("SAW")]
        [DatabaseDisplayName("SAW")]
        [Description("Tabelle Segatrice")]
        [RelatedFilter(DatabaseDisplayNameEnum.MaterialType, isMainFilter: true, priority: 1)]
        [RelatedFilter(DatabaseDisplayNameEnum.MinThickness, priority: 2)]
        [RelatedFilter(DatabaseDisplayNameEnum.MaxThickness, priority: 3)]
        [SuggestedFormat("${MinThickness} - ${MaxThickness}")]
        Saw = 32,
    }

    public static class ToolRangeTypeExtensions
    {
        public static SubRangeTypeEnum ToSubRangeType(this ToolRangeTypeEnum toolRangeType)
        {
            SubRangeTypeEnum subRangeType = SubRangeTypeEnum.None;
            switch(toolRangeType)
            {
                case ToolRangeTypeEnum.Bevel:
                    subRangeType = SubRangeTypeEnum.Bevel;
                    break;
                case ToolRangeTypeEnum.TrueHole:
                    subRangeType = SubRangeTypeEnum.TrueHole;
                    break;
                case ToolRangeTypeEnum.Mark:
                    subRangeType = SubRangeTypeEnum.Mark;
                    break;
            }

            return subRangeType;
        }
    }
}