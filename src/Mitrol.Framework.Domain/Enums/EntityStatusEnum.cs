namespace Mitrol.Framework.Domain.Enums
{
    public enum EntityStatusEnum
    {
        /// <summary>
        /// Disponibile
        /// </summary>
        Available = 1,

        /// <summary>
        /// Non disponibile
        /// </summary>
        Unavailable = 2,

        /// <summary>
        /// Attenzione (es. vita in esaurimento)  Solo per i tool
        /// </summary>
        Warning = 3,

        /// <summary>
        /// Allarme (es. vita esaurita) Solo per i tool
        /// </summary>
        Alarm = 4,

        /// <summary>
        /// Non visualizzare nessuna icona 
        /// </summary>
        NoIconToDisplay = 5,

        /// <summary>
        /// Da cancellare
        /// </summary>
        ToBeDeleted = 6
    }
}
