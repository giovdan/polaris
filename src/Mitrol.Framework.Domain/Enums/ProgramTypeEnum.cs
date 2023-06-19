namespace Mitrol.Framework.Domain.Enums
{
    using System.ComponentModel;

    /// <summary>
    /// Program Type Enumeration
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue(ProgramTypeEnum.G_97)]
    public enum ProgramTypeEnum : int
    {
        /// <summary>
        /// Sequenza pezzi
        /// </summary>
        [EnumSerializationName("PiecesSequence")]
        G_93 = 93,
        /*
        /// <summary>
        /// Nesting di tagli (solo per linee di taglio)
        /// </summary>
        G_94 = 94,
        */
        /// <summary>
        /// Nesting lineare
        /// </summary>
        [EnumSerializationName("LinearNesting")]
        G_95 = 95,

        /// <summary>
        /// Pezzo a misura
        /// </summary>
        [EnumSerializationName("PieceToMeasure")]
        G_97 = 97,

        /// <summary>
        /// Nesting piastre
        /// </summary>
        [EnumSerializationName("PlateNesting")]
        G_98 = 98,
    }
}
