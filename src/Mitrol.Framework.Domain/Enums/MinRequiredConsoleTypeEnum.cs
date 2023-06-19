namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipologia di apparecchiatura per il taglio al plasma Hypertherm
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("NotSpecified")]
    public enum MinRequiredConsoleTypeEnum : int
    {
        /// <summary>
        /// Non specificato
        /// </summary>
        [EnumSerializationName("NotSpecified")]
        [EnumField("Non specificato", true, "LBL_MINREQCON_NOTSPECIFIED")]
        NotSpecified = -1,

        /// <summary>
        /// Console Core (base)
        /// </summary>
        /// 
        [EnumSerializationName("Core")]
        [EnumCustomName("Core")]
        [EnumField("Console Core", true, "LBL_MINREQCON_CORE")] 
        Core = 0,

        /// <summary>
        /// Console Vwi
        /// </summary>
        [EnumSerializationName("VWi")]
        [EnumCustomName("VWi")]
        [EnumField("Console Vwi", true, "LBL_MINREQCON_VWI")]
        Vwi = 1,

        /// <summary>
        /// Console OptiMix
        /// </summary>
        [EnumSerializationName("OptiMix")]
        [EnumCustomName("OptiMix")]
        [EnumField("Console OptiMix", true, "LBL_MINREQCON_OPTIMIX")]
        OptiMix = 2
    }
}