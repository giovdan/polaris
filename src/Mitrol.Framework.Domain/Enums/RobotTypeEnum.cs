namespace Mitrol.Framework.Domain.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// Configurazione robot di taglio
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum RobotTypeEnum
    {
        /// <summary>
        /// Non configurato
        /// </summary>
        None = 0,

        /// <summary>
        /// Robot FICEP con Ossitaglio
        /// </summary>
        [EnumSerializationName("Oxy")]
        FIC = 1,

        /// <summary>
        /// Robot FICEP con Ossitaglio e Plasma
        /// </summary>
        [EnumSerializationName("OxyPla")]
        FICP = 3,

        /// <summary>
        /// Robot FICEP 6 assi con Ossitaglio
        /// </summary>
        [EnumSerializationName("Oxy6Axes")]
        FIC6O = 4,

        /// <summary>
        /// Robot FICEP 6 assi con Plasma
        /// </summary>
        [EnumSerializationName("Pla6Axes")]
        FIC6P = 5,

        /// <summary>
        /// Robot FICEP 6 assi con Ossitaglio e Plasma
        /// </summary>
        [EnumSerializationName("PlaOxy6Axes")]
        FIC6B = 6,

        [EnumSerializationName("FanucAnthropomorphous")]
        FanucAnthropomorphous = 7
    }
}
