
namespace Mitrol.Framework.Domain.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// Tipolgia di riferimento dell'origine del pezzo (Excalibur)
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("Initial")]
    public enum PieceOriginTypeEnum : int
    {
        /// <summary>
        /// Iniziale OP0
        /// </summary>
        [EnumSerializationName("Initial")]
        ORG_INI = 0,

        /// <summary>
        /// Finale   OP1
        /// </summary>
        [EnumSerializationName("Final")]
        ORG_FIN = 1,

        /// <summary>
        /// Centrale OP2
        /// </summary>
        [EnumSerializationName("Central")]
        ORG_CEN = 2,

        /// <summary>
        /// Iniziale OP3 con quota OCX
        /// </summary>
        [EnumSerializationName("InitialOCX")]
        ORG_INI_OCX = 3,

        /// <summary>
        /// Opposta (viene utilizzata solo da PROCESS se ci sono fori riferiti al lato opposto di OP0 o OP1)
        /// </summary>
        [EnumSerializationName("Opposite")]
        ORG_OPP = 10
    }
}
