namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Modalità di interpolazione degli assi sul percorso
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("None")]
    public enum InterpolationTypeEnum
    {
        /// <summary>
        /// Interpolazione non definita
        /// </summary>
        [EnumSerializationName("None")]
        [EnumField("Interpolazione non definita", true, "LBL_INTERPOLATIONTYPE_NONE")]
        None = 0,

        /// <summary>
        /// Interpolazione in CONTINUO
        /// </summary>
        [EnumSerializationName("Continous")]
        [EnumField("Interpolazione in CONTINUO", true, "LBL_INTERPOLATIONTYPE_CONTINOUS")]
        Continue = 72,

        /// <summary>
        /// Interpolazione in PUNTO A PUNTO
        /// </summary>
        [EnumSerializationName("PointToPoint")]
        [EnumField("Interpolazione in CONTINUO", true, "LBL_INTERPOLATIONTYPE_POINTTOPOINT")]
        PointToPoint = 73
    }
}
