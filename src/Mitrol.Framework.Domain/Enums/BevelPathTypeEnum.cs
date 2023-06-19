namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipi di percorso bevel
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("Multiple")]
    public enum BevelPathTypeEnum
    {
        /// <summary>
        /// Multiplo
        /// </summary>
        [EnumSerializationName("Multiple")]
        [EnumCustomName("Multiple")]
        [EnumField("Multiple", true, "LBL_BEVELPATHTYPE_MULTIPLE")]
        Multiple,

        /// <summary>
        /// Taglio bevel inferiore
        /// </summary>
        [EnumCustomName("SingleBottom")]
        [EnumSerializationName("SingleBottom")]
        [EnumField("Taglio bevel inferiore", true, "LBL_BEVELPATHTYPE_SINGLEBOTTOM")]
        SingleBottom,

        /// <summary>
        /// Taglio bevel diritto
        /// </summary>
        [EnumCustomName("SingleStraight")]
        [EnumSerializationName("SingleStraight")]
        [EnumField("Taglio bevel diritto", true, "LBL_BEVELPATHTYPE_SINGLESTRAIGHT")]
        SingleStraight,

        /// <summary>
        /// Taglio bevel superiore
        /// </summary>
        [EnumCustomName("SingleTop")]
        [EnumSerializationName("SingleTop")]
        [EnumField("Taglio bevel superiore", true, "LBL_BEVELPATHTYPE_SINGLETOP")]
        SingleTop
    }
}
