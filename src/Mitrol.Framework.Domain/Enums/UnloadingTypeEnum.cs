namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Modalità di scarico del pezzo
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("None")]
    public enum UnloadingTypeEnum
    {
        /// <summary>
        /// nessuno scarico
        /// </summary>
        [EnumSerializationName("None")]
        [EnumField("nessuno scarico", true, "LBL_UNLOADINGTYPE_NONE")]
        None = 0,

        /// <summary>
        /// Automatico
        /// </summary>
        [EnumSerializationName("Automatic")]
        [EnumField("Automatico", true, "LBL_UNLOADINGTYPE_AUTOMATIC")]
        Automatic = 15,

        /// <summary>
        /// Manuale
        /// </summary>
        [EnumSerializationName("Manual")]
        [EnumField("Manuale", true, "LBL_UNLOADINGTYPE_MANUAL")]
        Manual = 16,

        /// <summary>
        /// Automatico nella zona indicata da UnloadingCode
        /// </summary>
        [EnumSerializationName("AutomaticCode")]
        [EnumField("Automatico nella zona indicata da UnloadingCode", true, "LBL_UNLOADINGTYPE_AUTOMATICCODE")]
        AutomaticCode = 17,

        /// <summary>
        /// Automatico con carrino ausiliario
        /// </summary>
        [EnumSerializationName("AutomaticAuxCarriage")]
        [EnumField("Automatico con carrino ausiliario", true, "LBL_UNLOADINGTYPE_AUTOMATICAUXCARRIAGE")]
        AutomaticAuxCarriage = 18
    }
}
