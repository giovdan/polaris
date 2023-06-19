using Mitrol.Framework.Domain.Attributes;
using System.ComponentModel;

namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Modalità di compensazione utensile (tagli o fresature)
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("None")]
    public enum CompensationTypeEnum
    {
        /// <summary>
        /// Non selezionata
        /// </summary>
        [EnumSerializationName("NotSelected")]
        [EnumField("Non selezionata", true, "LBL_COMPENSATIONTYPE_NOTSELECTED")]
        NotSelected = 0,

        /// <summary>
        /// Nessuna compensazione
        /// </summary>
        [EnumSerializationName("None")]
        [EnumField("Nessuna compensazione", true, "LBL_COMPENSATIONTYPE_NONE")]
        None = 40,

        /// <summary>
        /// Compensazione sinistra (utensile a sinistra del percorso programmato)
        /// </summary>
        [EnumSerializationName("Left")]
        [EnumField("Compensazione sinistra", true, "LBL_COMPENSATIONTYPE_LEFT")]
        Left = 41,

        /// <summary>
        /// Compensazione destra (utensile a destra del percorso programmato)
        /// </summary>
        [EnumSerializationName("Right")]
        [EnumField("Compensazione sinistra", true, "LBL_COMPENSATIONTYPE_RIGHT")]
        Right = 42
    }
}
