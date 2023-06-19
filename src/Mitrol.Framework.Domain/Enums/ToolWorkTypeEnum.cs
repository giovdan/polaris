namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipologie di lavorazione utensili
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("Standard")]
    public enum ToolWorkTypeEnum
    {
        Undefined = -1,
        /// <summary>
        /// Lavorazione 'standard' (per tutti gli utensili DRILL tranne TS62)
        /// </summary>
        [EnumSerializationName("Standard")]
        [EnumCustomName("Standard")]
        [EnumField("Standard", true, "LBL_TOOLWRKTYPE_STD")]
        Standard=0,

        /// <summary>
        /// Lavorazione 'pesante' (TS35, TS73, TS38)
        /// </summary>
        [EnumCustomName("Heavy")]
        [EnumSerializationName("Heavy")]
        [EnumField("Lavorazione pesante", true, "LBL_TOOLWRKTYPE_HEAVY")]
        Heavy = 1,

        /// <summary>
        /// Discesa con avanzamento a tuffo (TS62)
        /// </summary>
        [EnumCustomName("Plunging")]
        [EnumSerializationName("Plunging")]
        [EnumField("Avanzamento a tuffo", true, "LBL_TOOLWRKTYPE_PLUNG")]
        Plunging = 2,

        /// <summary>
        /// Discesa con avanzamento in rampa (TS62)
        /// </summary>
        [EnumCustomName("Ramping")]
        [EnumSerializationName("Ramping")]
        [EnumField("Avanzamento in rampa", true, "LBL_TOOLWRKTYPE_RAMP")]
        Ramping = 3,
    }
}
