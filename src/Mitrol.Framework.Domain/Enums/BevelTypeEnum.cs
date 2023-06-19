namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipologie di tagli bevel
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("Straight")]
    public enum BevelTypeEnum
    {
        /// <summary>
        /// Taglio bevel diritto 0°
        /// </summary>
        [EnumSerializationName("*")]
        [EnumCustomName("*")]
        [EnumField("Taglio bevel diritto 0°", true, "LBL_BEVELTYPE_0")]
        Straight=0,

        /// <summary>
        /// Taglio bevel bottom
        /// </summary>
        [EnumCustomName("a")]
        [EnumSerializationName("A")]
        [EnumField("Taglio bevel bottom", true, "LBL_BEVELTYPE_A")]
        A,

        /// <summary>
        /// Taglio bevel bottom per i tagli combinati
        /// </summary>
        [EnumCustomName("b")]
        [EnumSerializationName("B")]
        [EnumField("Taglio bevel bottom per i tagli combinati", true, "LBL_BEVELTYPE_B")]
        B,

        /// <summary>
        /// Taglio bevel bottom+topY
        /// </summary>
        [EnumCustomName("k")]
        [EnumSerializationName("K")]
        [EnumField(" Taglio bevel bottom+topY", true, "LBL_BEVELTYPE_K")]
        K,

        /// <summary>
        /// Taglio bevel top
        /// </summary>
        [EnumCustomName("v")]
        [EnumSerializationName("V")]
        [EnumField(" Taglio bevel top", true, "LBL_BEVELTYPE_V")]
        V,

        /// <summary>
        /// Taglio bevel topY
        /// </summary>
        [EnumCustomName("y")]
        [EnumSerializationName("Y")]
        [EnumField(" Taglio bevel topY", true, "LBL_BEVELTYPE_Y")]
        Y,
    }
}
