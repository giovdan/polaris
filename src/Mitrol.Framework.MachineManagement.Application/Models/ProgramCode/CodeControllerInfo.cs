namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class CodeControllerInfo
    {
        /// <summary>
        /// Numero di linea corrente in esecuzione
        /// </summary>
        [JsonProperty("ExecutionLineNumber")]
        public int ExecutionLineNumber { get; set; }

        /// <summary>
        /// Numero di linee totali del ProgramCode
        /// </summary>
        [JsonProperty("TotalLinesNumber")]
        public int TotalLinesNumber { get; set; }

        /// <summary>
        /// Percentuale di avanzamento dell'esecuzione del program code
        /// </summary>
        [JsonProperty("ProgressPercentage")]
        public int ProgressPercentage { get; set; }

        /// <summary>
        /// Tempo rimanente espresso in minuti
        /// </summary>
        [JsonProperty("RemainingTime")]
        public int RemainingTime { get; set; }

        /// <summary>
        /// Identificativo del programma/productionRow che ha generato il ProgramCode corrente
        /// </summary>
        [JsonProperty("Id")]
        public long Id { get; set; }

        /// <summary>
        /// Nome del programma/productionRow che ha generato il ProgramCode corrente
        /// </summary>
        [JsonProperty("Name")]
        public string Name { get; set; }

        /// <summary>
        /// Numero di utensili richiesti dal programma
        /// </summary>
        [JsonProperty("NumberOfTools")]
        public int NumberOfTools { get; set; }

        /// <summary>
        /// Tipo di profilo del programma/productionRow che ha generato il ProgramCode corrente
        /// </summary>
        [JsonProperty("ProfileType")]
        public ProfileTypeEnum ProfileType { get; set; }

        /// <summary>
        /// Formattazione suggerita per gli attributi di tipo code generators.
        /// </summary>
        [JsonProperty("SuggestedCodeFormat")]
        public string SuggestedCodeFormat { get; set; }

        /// <summary>
        /// Attributi per la generazione del codice identificativo del materiale (dimensioni reali)
        /// </summary>
        [JsonProperty("CodeGenerators")]
        public Dictionary<string, object> CodeGenerators { get; set; }
    }
}
