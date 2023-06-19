namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Enumerato che definisce il tipo di segatrice
    /// </summary>
    public enum SawingMachineTypeEnum
    {
        /// <summary>
        /// Non configurata (DEFAULT)
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Sirio Mitrol (con asse di rotazione)
        /// </summary>
        SirioRotationSaw = 1,
        
        /// <summary>
        /// Sirio Mitrol (senza asse di rotazione)
        /// </summary>
        SirioFixedSaw = 2,
        
        /// <summary>
        /// Segatrice dal basso lato dx (impianti HPS)
        /// </summary>
        BottomSaw = 3,
        
        /// <summary>
        /// segatrice MEP
        /// </summary>
        MepSaw = 4,

        /// <summary>
        /// Segatrice con rotazione ad asse (integrata nel cnc) 
        /// </summary>
        RotationSaw = 5,

        /// <summary>
        /// Segatrice fissa senza rotazione (integrata nel cnc) 
        /// </summary>
        FixedSaw = 6,
        
        /// <summary>
        /// Segatrice fissa con rotazione a due posizioni
        /// </summary>
        FixedRotationTwoPositionSaw = 7,
    }
}
