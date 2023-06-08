using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Mitrol.Framework.Domain.Enums
{
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
        Straight = 0,

        /// <summary>
        /// Bevel
        /// </summary>
        [EnumCustomName("Bevel")]
        [EnumSerializationName("Bevel")]
        Bevel = 1
    }
}
