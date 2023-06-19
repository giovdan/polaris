namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Configurazione multistazione tandem (Gemini/Kronos)
    /// </summary>
    public enum TandemEnum
    {
        /// <summary>
        /// Tandem assente
        /// </summary>
        NONE = 0,

        /// <summary>
        /// Attivazione della modalità tandem MULTI-MASTER
        /// </summary>
        MULTIMASTER = 1,

        /// <summary>
        /// Attivazione della modalità tandem su impianto MASTER
        /// </summary>
        MASTER = 2,

        /// <summary>
        /// Attivazione della modalità tandem su impianto SLAVE
        /// </summary>
        SLAVE = 3
    }

}