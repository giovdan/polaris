namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Selezione dei gas di taglio del programma
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("NotSelected")]
    public enum GasTypeCutSelectionEnum
    {
        /// <summary>
        /// Not selected
        /// </summary>
        [EnumCustomName("NotSelected")]
        [EnumSerializationName("NotSelected")]
        [EnumField("Not selected", true, "LBL_GASTYPECUT_NOTSELECTED")]
        NotSelected = 0,

        /// <summary>
        /// Oxygen "O2"
        /// </summary>
        [EnumCustomName("O2")]
        [EnumSerializationName("O2")]
        [EnumField("Oxygen O2", true, "LBL_GASTYPECUT_O2")]
        O2 = 1,

        /// <summary>
        /// Air "Air"
        /// </summary>
        [EnumCustomName("Air")]
        [EnumSerializationName("Air")]
        [EnumField("Air", true, "LBL_GASTYPECUT_AIR")]
        Air = 5,

        /// <summary>
        /// Nitrogen "N2"
        /// </summary>
        [EnumCustomName("N2")]
        [EnumSerializationName("N2")]
        [EnumField("Nitrogen N2", true, "LBL_GASTYPECUT_N2")]
        N2 = 6,

        /// <summary>
        /// Nitrogen "N2"
        /// </summary>
        [EnumCustomName("H2O")]
        [EnumSerializationName("H2O")]
        [EnumField("Water H2O", true, "LBL_GASTYPECUT_H2O")]
        H2O = 12,

        /// <summary>
        /// Hydrogen "H2"
        /// </summary>
        [EnumCustomName("H2")]
        [EnumSerializationName("H2")]
        [EnumField("Hydrogen H2", true, "LBL_PLASMAGAS_H2")]
        H2 = 3,

        /// <summary>
        /// 95% nitrogen, 5% hydrogren "F5"
        /// </summary>
        [EnumCustomName("F5")]
        [EnumSerializationName("F5")]
        [EnumField("95% nitrogen, 5% hydrogren ", true, "LBL_PLASMAGAS_F5")]
        F5 = 11,

        /// <summary>
        /// Mix "Mix"
        /// </summary>
        [EnumCustomName("Mix")]
        [EnumSerializationName("Mix")]
        [EnumField("Mix", true, "LBL_PLASMAGAS_MIX")]
        Mix = 255
    }
}