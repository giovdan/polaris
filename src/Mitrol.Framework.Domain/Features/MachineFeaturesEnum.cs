namespace Mitrol.Framework.MachineManagement.Application.Enums
{
    using Mitrol.Framework.Domain.Features;

    /// <summary>
    /// Enumerato che rappresenta le features configurate sul CNC
    /// </summary>
    public enum MachineFeaturesEnum
    {
        /// <summary>
        /// Presenza setup origini (Valore di tipo booleano)
        /// </summary>
        [ContentType(typeof(bool), defaultValue: false)]
        SetupOrigins = 1,

        /// <summary>
        /// Modalità di lavoro per la Daily Production (Valore di tipo
        /// DailyProductionWorkingModeEnum
        /// </summary>
        [ContentType(typeof(DailyProductionWorkingModeEnum), defaultValue: DailyProductionWorkingModeEnum.None)]
        DailyProductionWorkingMode = 2,

        /// <summary>
        /// Presenza Handling Automatico
        /// </summary>
        [ContentType(typeof(bool), defaultValue: false)]
        AutomaticHandling = 3,

        /// <summary>
        /// Prenotazione programma
        /// </summary>
        [ContentType(typeof(bool), defaultValue: false)]
        ProgramReservation = 4,

        /// <summary>
        /// Caricamento programma
        /// </summary>
        [ContentType(typeof(bool), defaultValue: false)]
        ProgramLoading = 5,

        /// <summary>
        /// Visualizzazione sezione pezzi
        /// </summary>
        [ContentType(typeof(bool), defaultValue:true)]
        ShowPiecesList = 6,

        /// <summary>
        /// Possibilità di aggiungere/modificare programmi
        /// </summary>
        [ContentType(typeof(bool), defaultValue:true)]
        CanAddEditPrograms = 7

    }
}
