namespace Mitrol.Framework.Domain.Bus.Events
{
    using Newtonsoft.Json;

    public class CheckToolsForProductionRowEvent: Event
    {
        /// <summary>
        /// Totale degli utensili utilizzati per eseguire il programma
        /// </summary>
        [JsonProperty("NumberOfTools")]
        public int NumberOfTools { get; set; }

        /// <summary>
        /// Numero di utensili non disponibili per l'esecuzione
        /// </summary>
        [JsonProperty("NumberOfRightTools")]
        public int NumberOfRightTools { get; set; }

        [JsonProperty("ProductionRowId")]
        public long ProductionRowId { get; set; }

        public CheckToolsForProductionRowEvent()
        {
            ProductionRowId = 0;
            NumberOfTools = 0;
            NumberOfRightTools = 0;
        }
    }
}
