namespace Mitrol.Framework.Domain.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// Configurazione tipo di stozzatrici
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]

    public enum StozzaTypeEnum
    {
        /// <summary>
        /// Non configurato
        /// </summary>
        None = 0,

        /// <summary>
        /// Stozzatrice tradizionale
        /// </summary>
        [EnumSerializationName("Std")]
        STD = 1,

        /// <summary>
        /// Stozzatrice rotante
        /// </summary>
        [EnumSerializationName("Rot")]
        ROT = 2,

        /// <summary>
        /// Stozzatrice rotante polifunzionale
        /// </summary>
        [EnumSerializationName("RotPol")]
        ROT_POL = 3,

        /// <summary>
        /// Stozzatrice rotante monoassiale
        /// </summary>
        [EnumSerializationName("RotMono")]
        ROT_MONO = 4,

        /// <summary>
        /// Stozzatrice rotante polifunzionale
        /// </summary>
        [EnumSerializationName("Poli")]
        POLI = 5
    }
}
