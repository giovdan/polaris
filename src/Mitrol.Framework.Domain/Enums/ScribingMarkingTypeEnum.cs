namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipo di marcatura caratteri (Program)
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("Standard")]
    public enum ScribingMarkingTypeEnum
    {
        /// <summary>
        /// Standard (caratteri staccati)
        /// </summary>
        [EnumSerializationName("Standard")]
        [EnumField("Standard (caratteri staccati)", true, "LBL_SCRIBINGMARKINGTYPE_STANDARD")]
        Standard = 0,

        /// <summary>
        /// Caratteri collegati tra di loro (font speciale)
        /// </summary>
        [EnumSerializationName("LinkedCharacters")]
        [EnumField("Caratteri collegati tra di loro (font speciale)", true, "LBL_SCRIBINGMARKINGTYPE_LINKEDCHARACTERS")]
        LinkedCharacters = 1
    }
}
