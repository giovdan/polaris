namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Funzioni macchina (inserite nell'operazione MISC del pezzo)
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("None")]
    public enum Func
    {
        /// <summary>
        /// Nessuna funzione definita
        /// </summary>
        [EnumSerializationName("None")]
        [EnumCustomName("None")]
        [EnumField("Nessuna funzione definita", true, "LBL_FUNCTIONCODE_NONE")]
        None = 0,

        /// <summary>
        /// Stop programma incondizionato (M0 / G215)
        /// </summary>
        [EnumSerializationName("Stop")]
        [EnumCustomName("Stop")]
        [EnumField("Stop programma incondizionato (M0 / G215)", true, "LBL_FUNCTIONCODE_STOP")]
        Stop = 1,

        /// <summary>
        /// Stop programma condizionato es. da selettore plc (M1 / Fanuc G215)
        /// </summary>
        [EnumSerializationName("ConditionalStop")]
        [EnumCustomName("ConditionalStop")]
        [EnumField("Stop programma condizionato es. da selettore plc (M1 / Fanuc G215)", true, "LBL_FUNCTIONCODE_CONDITIONALSTOP")]
        ConditionalStop = 2,

        /// <summary>
        /// Spazzolatura automatica (T17 / Fanuc G250)
        /// </summary>
        [EnumSerializationName("AutomaticBrushing")]
        [EnumCustomName("AutomaticBrushing")]
        [EnumField("Spazzolatura automatica (T17 / Fanuc G250)", true, "LBL_FUNCTIONCODE_AUTOMATICBRUSHING")]
        AutomaticBrushing = 3,

        /// <summary>
        /// Spazzolatura manuale (T21)
        /// </summary>
        [EnumSerializationName("ManualBrushing")]
        [EnumCustomName("ManualBrushing")]
        [EnumField("Spazzolatura manuale (T21)", true, "LBL_FUNCTIONCODE_MANUALBRUSHING")]
        ManualBrushing = 4
    }
}
