namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Selezione dei gas di marcatura del programma
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("NotSelected")]
    public enum GasTypeMarkingSelectionEnum
    {
        /// <summary>
        /// Not selected
        /// </summary>
        [EnumCustomName("NotSelected")]
        [EnumSerializationName("NotSelected")]
        [EnumField("Not selected", true, "LBL_GASTYPEMARKING_NOTSELECTED")]
        NotSelected = 0,

        /// <summary>
        /// Nitrogen "N2"
        /// </summary>
        [EnumCustomName("N2")]
        [EnumSerializationName("N2")]
        [EnumField("Nitrogen N2", true, "LBL_GASTYPEMARKING_N2")]
        N2 = 6,

        /// <summary>
        /// Argon "Ar"
        /// </summary>
        [EnumCustomName("Ar")]
        [EnumSerializationName("Ar")]
        [EnumField("Argon Ar", true, "LBL_GASTYPEMARKING_ARG")]
        Ar = 7
    }
}