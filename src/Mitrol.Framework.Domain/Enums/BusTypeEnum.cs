namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Tipologia di bus di campo
    /// N.B. L'assegnazione del valore non deve essere modificata perchè viene utilizzata in APLR per la configurazione degli assi
    /// </summary>
    public enum BusTypeEnum
    {
        /// <summary>
        /// Linea CanBus asincrona
        /// </summary>
        CANBUS_ASYNC = 1,

        /// <summary>
        /// Linea CanBus sincrona
        /// </summary>
        CANBUS_SYNC = 2,

        /// <summary>
        /// Linea EtherCAT
        /// </summary>
        ETHERCAT = 3,
    }
}