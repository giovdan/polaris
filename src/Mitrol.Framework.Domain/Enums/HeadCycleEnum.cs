namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    /// <summary>
    /// Defizione dell'unità a cui è assegnata un'operazione del pezzo
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("Single")]
    public enum HeadCycleEnum : int
    {
        /// <summary>
        /// Lavorazione singola eseguita con una testa
        /// </summary>
        [EnumSerializationName("SingleHead")]
        [EnumField("Lavorazione singola eseguita con una testa", true, "LBL_HEADCYCLE_SINGLE")] 
        Single = 0,

        /// <summary>
        /// Lavorazione singola eseguita con l'unità C
        /// </summary>
        [EnumSerializationName("RightHead")] 
        [EnumField("Lavorazione singola eseguita con l'unità C", true, "LBL_HEADCYCLE_RIGHT")]
        Right = 1,

        /// <summary>
        /// Lavorazione singola eseguita con l'unità D
        /// </summary>
        [EnumSerializationName("LeftHead")] 
        [EnumField("Lavorazione singola eseguita con l'unità D", true, "LBL_HEADCYCLE_LEFT")]
        Left = 2,

        /// <summary>
        /// Lavorazione doppia (in copia) eseguita con entrambe le unità C e D
        /// </summary>
        [EnumSerializationName("BothHead")] 
        [EnumField("Lavorazione doppia eseguita con entrambe le unità C e D", true, "LBL_HEADCYCLE_BOTH")]
        Both = 3
    }
}
