namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Modalità di ordinamento dei prefori delle operazioni PATHC
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("GroupAhead")]
    public enum PreHolesOrderModalityEnum
    {
        /// <summary>
        /// I prefori vengono eseguiti insieme alle operazioni del gruppo che precede il PATHC
        /// </summary>
        [EnumSerializationName("GroupAhead")]
        [EnumField("GroupAhead", true, "LBL_PREHOLESEXECUTIONMODE_GROUPAHEAD")]
        GroupAhead = 0,

        ///<summary>
        /// I prefori vengono eseguiti insieme al PATHC (ovviamente prima il preforo poi il taglio)
        ///</summary>
        [EnumSerializationName("AttachedToCut")]
        [EnumField("AttachedToCut", true, "LBL_PREHOLESEXECUTIONMODE_ATTACHEDTOCUT")]
        AttachedToCut = 1
    }
}
