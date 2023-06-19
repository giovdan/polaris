namespace Mitrol.Framework.Domain.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// Tipo di marcatrice configurata
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum MarkingUnitConfigurationEnum
    {
        /// <summary>
        /// Non configurata
        /// </summary>
        [EnumSerializationName("None")]
        None = 0,

        /// <summary>
        /// Mk a cassetti
        /// </summary>
        [EnumSerializationName("Drawers")]
        Drawers = 1,

        /// <summary>
        /// Mk a disco con 36 caratteri
        /// </summary>
        [EnumSerializationName("Disk36")]
        Disk36 = 2,

        /// <summary>
        /// Mk a disco con 40 caratteri (38 + 2 spazi vuoti per evitare strisciamento)
        /// </summary>
        [EnumSerializationName("Disk40")]
        Disk40 = 4,

        /// <summary>
        /// Getto d'inchiostro modello ReaJet
        /// </summary>
        [EnumSerializationName("Inkjet")]
        InkJet = 6,

        /// <summary>
        /// Getto d'inchiostro modello ReaJet con doppia testina
        /// </summary>
        [EnumSerializationName("InkjetDoubleHead")]
        InkJetDoubleHead = 7,
    }
}
