namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;
    
    /// <summary>
    /// Tipologia di utilizzo degli assi ausiliari per eseguire una certa operazione del pezzo
    /// </summary>
    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("Active")]
    public enum AuxiliariesCycleEnum : int
    {

        /// <summary>
        /// Lavorazione con assi ausiliari senza congelamento
        /// </summary>
        [EnumSerializationName("Active")]
        [EnumField("Lavorazione con assi ausiliari senza congelamento", true, "LBL_AUXILIARIESCYCLE_ACTIVE")]
        Active = 0,

        /// <summary>
        /// Lavorazione senza assi ausiliari, viene usato l'asse X
        /// </summary>
        [EnumSerializationName("NotUsed")]
        [EnumField("Lavorazione senza assi ausiliari", true, "LBL_AUXILIARIESCYCLE_NOTUSED")]
        NotUsed = 1,

        /// <summary>
        /// Lavorazione con congelamento degli assi ausiliari automatico (M93)
        /// </summary>
        [EnumSerializationName("Freezing")]
        [EnumField("Lavorazione con assi ausiliari senza congelamento", true, "LBL_AUXILIARIESCYCLE_FREEZING")]
        Freezing = 2,
        
        /// <summary>
        /// Lavorazione con congelamento degli assi ausiliari al min (M63)
        /// </summary>
        [EnumSerializationName("FreezingToMinimum")]
        [EnumField("Lavorazione con assi ausiliari senza congelamento", true, "LBL_AUXILIARIESCYCLE_FREEZINGTOMINIMUM")]
        FreezingToMinimum = 3,
        
        /// <summary>
        /// Lavorazione con congelamento degli assi ausiliari al max (M64)
        /// </summary>
        [EnumSerializationName("FreezingToMaximum")]
        [EnumField("Lavorazione con assi ausiliari senza congelamento", true, "LBL_AUXILIARIESCYCLE_FREEZINGTOMAXIMUM")]
        FreezingToMaximum = 4
    }
}
