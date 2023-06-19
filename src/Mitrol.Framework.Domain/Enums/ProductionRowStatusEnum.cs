namespace Mitrol.Framework.Domain.Enums
{
    using System;

    [Flags()]
    public enum ProductionRowStatusEnum
    {
        /// <summary>
        /// Riga di produzione su cui non è stata effettuata nessuna operazione
        /// </summary>
        Empty = 1,
        
        /// <summary>
        /// Riga di produzione per cui è in corso l'elaborazione del canonico
        /// </summary>
        InProgress = 2,
        
        /// <summary>
        /// Riga di produzione elaborata
        /// </summary>
        Processed = 4,

        /// <summary>
        /// Riga di produzione eseguita
        /// </summary>
        Executed = 8,

        /// <summary>
        /// Richiesta caricamento PLC
        /// </summary>
        RQLoad = 16,

        /// <summary>
        /// Caricamento in corso (ACK da PLC)
        /// </summary>
        RQAck = 32,

        /// <summary>
        /// Barra non disponibile (esito non OK caricamento PLC)
        /// NoDisp
        /// </summary>
        NotAvailable = 64,

        /// <summary>
        /// Caricamento abortito (esito non OK caricamento PLC)
        /// Abort
        /// </summary>
        Aborted = 128,

        /// <summary>
        /// Magazzino non pronto (ACK da PLC)
        /// </summary>
        NotReady = 256
    }
}
