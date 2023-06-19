namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipologia di taglio al plasma (usato per definire i record della tabella plasma)
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("Cut")]
    public enum PlasmaCutTypeEnum
    {
        /// <summary>
        /// Taglio
        /// </summary>
        [EnumField("Taglio", true, "LBL_CUT")]
        Cut = 0,

        /// <summary>
        /// Marcatura
        /// </summary>
        [EnumField("Marcatura", true, "LBL_MARK")]
        Mark = 1,

        /// <summary>
        /// Taglio underWater
        /// </summary>
        [EnumField("Taglio underWater", true, "LBL_UNDERWATER")] 
        [EnumCustomName("Underwater")]
        UnderWater = 2
    }

}
