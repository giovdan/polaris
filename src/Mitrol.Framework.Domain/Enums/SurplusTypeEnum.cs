namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipologia di eccedenza (Program)
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("FinalEdge")]
    public enum SurplusTypeEnum
    {
        /// <summary>
        /// Eccedenza in coda (pinza)
        /// </summary>
        [EnumSerializationName("FinalEdge")]
        [EnumField("Eccedenza in coda(pinza)" ,true, "LBL_SURPLUSTYPE_INTAIL")]
        FinalEdge = 0,

        /// <summary>
        /// Eccedenza in testa
        /// </summary>
        [EnumSerializationName("LeadingEdge")]
        [EnumField("Eccedenza in testa", true, "LBL_SURPLUSTYPE_INHEAD")]
        LeadingEdge = 1,
    }
}
