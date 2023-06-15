
namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Core;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum AttributeDefinitionEnum
    {
        None = 0,
        [Description("Soglia affilatura punta [mt]")]
        [EnumSerializationName("WarningToolLife")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.WarningToolLife)]
        WarningToolLife = 8,

        [Description("Massima vita utensile corrente [mt]")]
        [EnumSerializationName("MaxToolLife")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaxToolLife)]
        MaxToolLife = 9,

        [Description("Vita utensile corrente [mt]")]
        [EnumSerializationName("ToolLife")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolLife)]
        ToolLife = 10,

        [Description("Prenotazione automatica del sensitivo [n]")]
        [EnumSerializationName("AutoSensitiveEnable")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.AutoSensitiveEnable)]
        AutoSensitiveEnable = 13,

        [Description("Lunghezza utensile [mm]")]
        [EnumSerializationName("ToolLength")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolLength)]
        ToolLength = 19,

        [Description("Tempo di vita ugello [sec]")]
        [EnumSerializationName("NozzleLifeTime")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeTime)]
        NozzleLifeTime = 86,

        [Description("Vita ugello nr. accensioni [n]")]
        [EnumSerializationName("NozzleLifeIgnitions")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeIgnitions)]
        NozzleLifeIgnitions = 87,

        [Description("N° accensioni OK [n]")]
        [EnumSerializationName("NozzleLifeIgnitionsSuccess")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeIgnitionsSuccess)]
        NozzleLifeIgnitionsSuccess = 88,

        [Description("N° accensioni non OK [n]")]
        [EnumSerializationName("NozzleLifeIgnitionsFailed")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeIgnitionsFailed)]
        NozzleLifeIgnitionsFailed = 89,

        [Description("N° massimo accensioni [n]")]
        [EnumSerializationName("NozzleLifeMaxIgnitions")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeMaxIgnitions)]
        NozzleLifeMaxIgnitions = 90,

        [Description("Soglia di attenzione nr. accensioni [n]")]
        [EnumSerializationName("NozzleLifeWarningLimitIgnitions")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeWarningLimitIgnitions)]
        NozzleLifeWarningLimitIgnitions = 91,

        [Description("Soglia di attenzione Tempo di vita ugello [sec]")]
        [EnumSerializationName("NozzleLifeWarningTime")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeWarningTime)]
        NozzleLifeAttentionThreshold = 143,

        [Description("Tempo di vita massima uggello")]
        [EnumSerializationName("NozzleLifeMaxTime")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.NozzleLifeMaxTime)]
        MaxNozzleLifeTime = 144,

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

        [Description("Tipologia di tool")]
        [EnumSerializationName("TS")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.TS)]
        ToolType = 254,

        [Description("Tool Type per Probe Technology")]
        [EnumSerializationName("ProbeTS")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.ProbeTS)]
        ProbeTS = 298,

        [Description("Tool Type per Tecnologia pre foro")]
        [EnumSerializationName("PreHoleTS")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.PreHoleTS)]
        PreHoleTS = 301,

        [Description("Soglia preallarme vita lama")]
        [EnumSerializationName("WarningBladeLife")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.WarningBladeLife)]
        WarningBladeLife = 376,

        [Description("Massima vita lama")]
        [EnumSerializationName("MaxBladeLife")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.MaxBladeLife)]
        MaxBladeLife = 377,

        [Description("Vita lama")]
        [EnumSerializationName("BladeLife")]
        [DatabaseDisplayName(DatabaseDisplayNameEnum.BladeLife)]
        BladeLife = 378,


    }
}
