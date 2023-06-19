namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("FlangesDownward")]
    public enum PuTypeEnum : int
    {
        [EnumSerializationName("NotDefined")]
        [EnumField("Non Definito", true, "LBL_PUTYPEENUM_NOTDEFINED")]
        NotDefined = 0,
        
        /// <summary>
        /// Ali sui rulli
        /// </summary>
        [EnumSerializationName("FlangesDownward")]
        [EnumField("Ali sui rulli", true, "LBL_PUTYPEENUM_FLANGESDOWNWARD")]
        FlangesDownward = 1,

        /// <summary>
        /// Ali verso l'alto
        /// </summary>
        [EnumSerializationName("FlangesUpward")]
        [EnumField("Ali verso l'alto", true, "LBL_PUTYPEENUM_FLANGESUPWARD")]
        FlangesUpward = 2,
    }
}
