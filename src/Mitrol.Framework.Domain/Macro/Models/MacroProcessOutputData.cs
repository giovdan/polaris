namespace Mitrol.Framework.Domain.Macro
{
    using System.Collections.Generic;
    /// <summary>
    /// Classe da utilizzare per la raccolta dei dati prodotta da librerie esterne 
    /// </summary>
    public class MacroOutputData: ExternalBaseData
    {
        /// <summary>
        /// Lista delle operazioni ottenuta dall'elaborazione della macro
        /// </summary>
        public List<IMacroOperationItem> MacroProcessedOperations { get; set; }

        public MacroOutputData()
        { }
    }

    
}
