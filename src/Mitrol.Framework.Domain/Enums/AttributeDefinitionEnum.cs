
namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Core;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum AttributeDefinitionEnum
    {
        None = 0,
        [Description("Utensile abilitato per unità A")]
        [EnumSerializationName("ToolEnableA")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolEnableA)]
        [BitEnableFor(UnitEnum.A)]
        ToolEnableA = 159,

        [Description("Utensile abilitato per unità B")]
        [EnumSerializationName("ToolEnableB")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolEnableB)]
        [BitEnableFor(UnitEnum.B)]
        ToolEnableB = 160,

        [Description("Utensile abilitato per unità C")]
        [EnumSerializationName("ToolEnableC")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolEnableC)]
        [BitEnableFor(UnitEnum.C)]
        ToolEnableC = 161,

        [Description("Utensile abilitato per unità D")]
        [EnumSerializationName("ToolEnableD")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolEnableD)]
        [BitEnableFor(UnitEnum.D)]
        ToolEnableD = 162,
    }
}
