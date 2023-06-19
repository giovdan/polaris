
namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipolgia di riferimento delle coordinate di programmazione di una operazione del pezzo
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("Standard")]
    public enum OperationOriginTypeEnum : int
    {
        /// <summary>
        /// Origine di programmazione standard (in basso a sinistra)
        /// </summary>
        [EnumSerializationName("Standard")]
        [EnumField("Origine di programmazione standard (in basso a sinistra)", true, "LBL_ORIGINTYPE_ACTIVE")]
        Standard = 0,

        /// <summary>
        /// Lato opposto dell'origine di programmazione standard
        /// </summary>
        [EnumSerializationName("Opposite")]
        [EnumField("Lato opposto dell'origine di programmazione standard", true, "LBL_ORIGINTYPE_OPPOSITE")]
        Opposite = 1,

        /// <summary>
        /// Mezzeria semiala superiore
        /// </summary>
        [EnumSerializationName("CenterlineTop")]
        [EnumField("Mezzeria semiala superiore", true, "LBL_ORIGINTYPE_CENTERLINETOP")]
        CenterlineTop = 2,

        /// <summary>
        /// Mezzeria semiala inferiore
        /// </summary>
        [EnumSerializationName("CenterlineBottom")]
        [EnumField("Mezzeria semiala inferiore", true, "LBL_ORIGINTYPE_CENTERLINEBOTTOM")]
        CenterlineBottom = 3,

        /// <summary>
        /// Mezzeria semiala superiore ed inferiore
        /// </summary>
        [EnumSerializationName("CenterlineTopBottom")]
        [EnumField("Mezzeria semiala superiore ed inferiore", true, "LBL_ORIGINTYPE_CENTERLINETOPBOTTOM")]
        CenterlineTopBottom = 4
    }
}
