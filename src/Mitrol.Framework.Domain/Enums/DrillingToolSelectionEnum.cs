namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Criterio di scelta in elaborazione degli utensili di foratura dal ToolManagement (quando ci sono utensili uguali)
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("ByMaxLife")]
    public enum DrillingToolSelectionEnum : int
    {
        /// <summary>
        /// Priorità vita utensile massima
        /// </summary>
        [EnumSerializationName("ByMaxLife")]
        [EnumField("Priorità vita utensile massima", true, "LBL_DRILLTOOLFILTER_BYMAXLIFE")]
        ByMaxLife = 0,

        /// <summary>
        /// Priorità vita utensile minima
        /// </summary>
        [EnumSerializationName("ByMinLife")]
        [EnumField("Priorità vita utensile minima", true, "LBL_DRILLTOOLFILTER_BYMINLIFE")]
        ByMinLife = 1
    }
}
