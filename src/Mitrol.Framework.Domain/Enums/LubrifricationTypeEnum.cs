namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipologia di lubrificazione punte (usato nelle unità DRILL)
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("LUB_0")]
    public enum LubrificationTypeEnum : int
    {
        /// <summary>
        /// Nessuna lubrificazione
        /// </summary>
        [EnumSerializationName("0")]
        [EnumField("Nessuna lubrificazione", true, "LBL_LUB_0")]
        LUB_0 = 0,

        /// <summary>
        /// Esterna solo aria
        /// </summary>
        [EnumSerializationName("1")]
        [EnumField("Esterna solo aria", true, "LBL_LUB_1")]
        LUB_1 = 1,

        /// <summary>
        /// Interna solo aria
        /// </summary>
        [EnumSerializationName("2")]
        [EnumField("Interna solo aria", true, "LBL_LUB_2")]
        LUB_2 = 2,

        /// <summary>
        /// Interna + Esterna solo aria
        /// </summary>
        [EnumSerializationName("3")]
        [EnumField("Interna + Esterna solo aria", true, "LBL_LUB_3")]
        LUB_3 = 3,

        /// <summary>
        /// Interna solo Lubrificante
        /// </summary>
        [EnumSerializationName("4")]
        [EnumField("Interna solo Lubrificante", true, "LBL_LUB_4")]
        LUB_4 = 4,

        /// <summary>
        /// Interna + Esterna solo Lubrificante
        /// </summary>
        [EnumSerializationName("5")]
        [EnumField("Interna + Esterna solo Lubrificante", true, "LBL_LUB_5")]
        LUB_5 = 5,

        /// <summary>
        /// Esterna lubrificante
        /// </summary>
        [EnumSerializationName("6")]
        [EnumField("Esterna lubrificante", true, "LBL_LUB_6")]
        LUB_6 = 6,
    }
}
