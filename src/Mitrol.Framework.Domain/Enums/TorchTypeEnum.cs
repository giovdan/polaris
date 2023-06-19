namespace Mitrol.Framework.Domain.Enums
{
    // Definizione dei tipi di torce possibili
    public enum TorchTypeEnum
    {
        /// <summary>
        /// Nessuna torcia
        /// </summary>
        NONE = 0,
        
        /// <summary>
        /// Standard diritta
        /// </summary>
        STD = 1,
        
        /// <summary>
        /// Bevel
        /// </summary>
        BEVEL = 2,
        
        /// <summary>
        /// Multipla, Master/Slave (NTORC= definisce il numero di slave)
        /// </summary>
        MSTSLV = 3,
        
        /// <summary>
        /// Bevel tripla
        /// </summary>
        BEVELT = 4
    }
}
