namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Modalità di elaborazione (Program)
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("PRC_0")]
    public enum ProcessingModalityEnum
    {
        /// <summary>
        /// Nessuna (Standard per alcuni impianti)
        /// </summary>
        [EnumSerializationName("0")]
        [EnumField("Nessuna (Standard per alcuni impianti)", true, "LBL_PRC_0")]
        PRC_0 = 0,

        /// <summary>
        /// Passes
        /// </summary>
        [EnumSerializationName("1")]
        [EnumField("Passes", true, "LBL_PRC_1")]
        PRC_1 = 1,

        /// <summary>
        /// Single pass
        /// </summary>
        [EnumSerializationName("2")]
        [EnumField("Single pass", true, "LBL_PRC_2")]
        PRC_2 = 2,

        /// <summary>
        /// Single pass for piece
        /// </summary>
        [EnumSerializationName("3")]
        [EnumField("Single pass for piece", true, "LBL_PRC_3")]
        PRC_3 = 3,

        /// <summary>
        /// Passes + cuts (solo per 306PS)
        /// </summary>
        [EnumSerializationName("4")]
        [EnumField("Passes + cuts", true, "LBL_PRC_4")]
        PRC_4 = 4,

        /// <summary>
        /// Passes + under-passes
        /// </summary>
        [EnumSerializationName("5")]
        [EnumField("Passes + under-passes", true, "LBL_PRC_5")]
        PRC_5 = 5,

        /// <summary>
        /// Single pass + under-passes
        /// </summary>
        [EnumSerializationName("6")]
        [EnumField("Single pass + under-passes", true, "LBL_PRC_6")]
        PRC_6 = 6,

        /// <summary>
        /// Programming order
        /// </summary>
        [EnumSerializationName("7")]
        [EnumField("Programming order", true, "LBL_PRC_7")]
        PRC_7 = 7,

        /// <summary>
        /// Passes + cuts for 'n' piece (solo per 504PS)
        /// </summary>
        [EnumSerializationName("8")]
        [EnumField("Passes + cuts for 'n' piece", true, "LBL_PRC_8")]
        PRC_8 = 8,

        /// <summary>
        /// Programming order with group X order
        /// </summary>
        [EnumSerializationName("9")]
        [EnumField("Programming order with group X order", true, "LBL_PRC_9")]
        PRC_9 = 9,

        /// <summary>
        /// Programming order with group cut order
        /// </summary>
        [EnumSerializationName("10")]
        [EnumField("Programming order with group cut order", true, "LBL_PRC_10")]
        PRC_10 = 10,

        /// <summary>
        /// Auxiliaries axis optimization
        /// </summary>
        [EnumSerializationName("11")]
        [EnumField("Auxiliaries axis optimization", true, "LBL_PRC_11")]
        PRC_11 = 11,
    }
}


