namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Modalità di eliminazione trucioli (Program)
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("None")]
    public enum ChipRemovalModalityEnum
    {
        /// <summary>
        /// Nessuno
        /// </summary>
        [EnumSerializationName("None")]
        [EnumField("Nessuno", true, "LBL_CHIPREMOVALMODE_NONE")]
        None = 0,

        /// <summary>
        /// Spazzolatura
        /// </summary>
        [EnumSerializationName("Brushing")]
        [EnumField("Spazzolatura", true, "LBL_CHIPREMOVALMODE_BRUSHING")]
        Brushing = 1,

        /// <summary>
        /// Aspirazione trucioli (solo per GEMINI Fanuc)
        /// </summary>
        [EnumSerializationName("ChipExtraction")]
        [EnumField("Aspirazione trucioli", true, "LBL_CHIPREMOVALMODE_CHIPEXTRACTION")]
        ChipExtraction = 2,

        /// <summary>
        /// Aspirazione + Spazzolatura (solo per GEMINI Fanuc)
        /// </summary>
        [EnumSerializationName("ExtractionAndBrushing")]
        [EnumField("Aspirazione + Spazzolatura", true, "LBL_CHIPREMOVALMODE_EXTRACTINGANDBRUSHING")]
        ExtractionAndBrushing = 3,
    }
}
