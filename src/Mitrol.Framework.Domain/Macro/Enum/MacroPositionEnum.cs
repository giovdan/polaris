namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Enumerato che definisce la posizione di una macro
    /// </summary>
    [DefaultValue("NotSpecified")]
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum MacroPositionEnum
    {
        /// <summary>
        /// Non specificata
        /// </summary>
        [EnumField("Non specificata", true, "LBL_MACROPOSITION_NOTSPECIFIED")]
        [EnumSerializationName("NotSpecified")]
        NotSpecified = 0,
        /// <summary>
        /// Iniziale
        /// </summary>
        [EnumField("Iniziale", true, "LBL_MACROPOSITION_ATBEGINNING")]
        [EnumSerializationName("AtBeginning")]
        AtBeginning = 1,
        /// <summary>
        /// Finale
        /// </summary>
        [EnumField("Finale", true, "LBL_MACROPOSITION_INTHEEND")]
        [EnumSerializationName("InTheEnd")]
        InTheEnd = 2
    }
}
