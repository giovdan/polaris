namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Tipologia di forma dell'ugello
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("Bevel")]
    public enum NozzleShapeEnum
    {
        /// <summary>
        /// Non definito
        /// </summary>
        NotDefined = -1,

        /// <summary>
        /// Diritto
        /// </summary>
       
        [EnumCustomName("Straight")]
        [EnumSerializationName("Straight")]
        [EnumField("Diritto", true, "LBL_NOZZLESHAPE_STRAIGHT")]
        Straight = 0,

        /// <summary>
        /// Bevel
        /// </summary>
        [EnumCustomName("Bevel")]
        [EnumSerializationName("Bevel")]
        [EnumField("Bevel", true, "LBL_NOZZLESHAPE_BEVEL")]
        Bevel = 1
    }
}
