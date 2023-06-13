namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipologie di gas utilizzate nelle apparecchiature di taglio al plasma Hypertherm XPR
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("None")]
    public enum GasTypeXprEnum
    {
        /// <summary>
        /// No Gas
        /// </summary>
        [EnumCustomName("None")]
        [EnumSerializationName("None")]
        [EnumField("No Gas", true, "LBL_PLASMAGAS_NONE")]
        None = 0,

        /// <summary>
        /// Oxygen "O2"
        /// </summary>
        [EnumCustomName("O2")]
        [EnumSerializationName("O2")]
        [EnumField("Oxygen O2", true, "LBL_PLASMAGAS_O2")]
        O2 = 1,

        /// <summary>
        /// Reserved "Reserved"
        /// </summary>
        [EnumCustomName("GAS2")]
        [EnumSerializationName("GAS2")]

        XPR_GAS_2 = 2,

        /// <summary>
        /// Hydrogen "H2"
        /// </summary>
        [EnumCustomName("H2")]
        [EnumSerializationName("H2")]
        [EnumField("Hydrogen H2", true, "LBL_PLASMAGAS_H2")]
        H2 = 3,

        /// <summary>
        /// Reserved "Reserved"
        /// </summary>
        [EnumCustomName("GAS4")]
        [EnumSerializationName("GAS4")]
        XPR_GAS_4 = 4,

        /// <summary>
        /// Air "Air"
        /// </summary>
        [EnumCustomName("Air")]
        [EnumSerializationName("Air")]
        [EnumField("Air", true, "LBL_PLASMAGAS_AIR")]
        Air = 5,

        /// <summary>
        /// Nitrogen "N2"
        /// </summary>
        [EnumCustomName("N2")]
        [EnumSerializationName("N2")]
        [EnumField("Nitrogen N2", true, "LBL_PLASMAGAS_N2")]
        N2 = 6,

        /// <summary>
        /// Argon "Ar"
        /// </summary>
        [EnumCustomName("Ar")]
        [EnumSerializationName("Ar")]
        [EnumField("Argon Ar", true, "LBL_PLASMAGAS_AR")]
        Ar = 7,

        /// <summary>
        /// Reserved "Reserved"
        /// </summary>
        [EnumCustomName("GAS8")]
        [EnumSerializationName("GAS8")]
        XPR_GAS_8 = 8,

        /// <summary>
        /// Reserved "Reserved"
        /// </summary>
        [EnumCustomName("GAS9")]
        [EnumSerializationName("GAS9")]
        XPR_GAS_9 = 9,

        /// <summary>
        /// Reserved "Reserved"
        /// </summary>
        [EnumCustomName("GAS10")]
        [EnumSerializationName("GAS10")]
        XPR_GAS_10 = 10,

        /// <summary>
        /// 95% nitrogen, 5% hydrogren "F5"
        /// </summary>
        [EnumCustomName("F5")]
        [EnumSerializationName("F5")]
        [EnumField("95% nitrogen, 5% hydrogren ", true, "LBL_PLASMAGAS_F5")]
        F5 = 11,

        /// <summary>
        /// Water for injection processes "H2O"
        /// </summary>
        [EnumCustomName("H2O")]
        [EnumSerializationName("H2O")]
        [EnumField("Water", true, "LBL_PLASMAGAS_H2O")]
        H2O = 12,

        /// <summary>
        /// Reserved "Reserved"
        /// </summary>
        [EnumCustomName("GAS13")]
        [EnumSerializationName("GAS13")]
        XPR_GAS_13 = 13,

        /// <summary>
        /// Mix "Mix"
        /// </summary>
        [EnumCustomName("Mix")]
        [EnumSerializationName("Mix")]
        [EnumField("Mix", true, "LBL_PLASMAGAS_MIX")]
        Mix = 255
    }
}