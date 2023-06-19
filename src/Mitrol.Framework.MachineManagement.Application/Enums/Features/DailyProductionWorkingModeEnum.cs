namespace Mitrol.Framework.MachineManagement.Application.Enums
{
    /// <summary>
    /// Enumerato che gestisce le modalità di lavoro della lista di prod
    /// </summary>
    public enum DailyProductionWorkingModeEnum
    {
        /// <summary>
        /// Default
        /// </summary>
        None = 0,

        /// <summary>
        /// Coda di programmi. Progettata per la modalità di lavoro con una tavola di appoggio ed il
        /// caricamento e scaricamento manuale delle lastre che devono essere lavorate (di cui prima
        /// devo rilevarne l'origine sulla tavola , da qui la presenza delle Origini nella Daily
        /// Production). Vengono lavorati uno o più programmi contemporaneamente (coda di
        /// programmi).
        /// </summary>
        Plates = 1,

        /// <summary>
        /// Lavorazione singola Progettata per la modalità di lavoro con un sistema di carico
        /// manuale o automatico, dove tipicamente è presente un banco di carico ed uno di scarico
        /// ed eventuale sistema di handling dei materiali (collegamento con altri impianti). Viene
        /// lavorato un programma per volta, ma nella sequeza automatica indicata dall'ordine nella
        /// lista
        /// </summary>
        SequenceAuto = 2,

        /// <summary>
        /// Sequenza Progettata per la modalità di lavoro con una banco di appoggio ed il
        /// caricamento e scaricamento manuale dei pezzi che devono essere lavorati (l'origine di
        /// ogni pezzo sul banco viene fatta automaticamente durante la lavorazione). Vengono
        /// lavorati uno o più pezzi nella sequenza indicata.
        /// </summary>
        SequenceManual = 3,
    }
}
