namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;
 
    /// <summary>
    /// Modalità di ordinamento delle operazioni del pezzo all'interno di un NODE
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("NotDefined")]
    public enum SequenceOrderTypeEnum : int
    {
        /// <summary>
        /// Non definito
        /// </summary>
        [EnumSerializationName("NotDefined")]
        [EnumField("Non definito", true, "LBL_SEQUENCEORDERTYPE_NOTDEFINED")]
        NotDefined = -1,

        /// <summary>
        /// Nessun ordinamento (ex MISC M60)
        /// </summary>
        [EnumSerializationName("None")]
        [EnumField("Nessun ordinamento", true, "LBL_SEQUENCEORDERTYPE_NONE")]
        None = 0,

        /// <summary>
        /// Operazioni ordinate per singolo piano di lavoro (ex MISC M61)
        /// </summary>
        [EnumSerializationName("SingleSide")]
        [EnumField("Operazioni ordinate per singolo piano di lavoro", true, "LBL_SEQUENCEORDERTYPE_SINGLESIDE")]
        SingleSide = 1,

        /// <summary>
        /// Operazioni ordinate per tutti su tutti i piani di lavoro (ex MISC M62)
        /// </summary>
        [EnumSerializationName("AllSide")]
        [EnumField("Operazioni ordinate per tutti su tutti i piani di lavoro", true, "LBL_SEQUENCEORDERTYPE_ALLSIDE")]
        AllSide = 2
    }
}
