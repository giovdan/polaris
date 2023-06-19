using System;

namespace Mitrol.Framework.Domain.Enums
{
    [Flags()]
    public enum LineModalityEnum
    {
        /// <summary>
        /// Modalità di esecuzione standard
        /// </summary>
        Standard = 0,

        /// <summary>
        /// Esecuzione con ottimizzazione assi ausiliari (doppia testa)
        /// </summary>
        OptimizedAuxiliaries = 1,

        /// <summary>
        /// Esecuzione con aspirazione trucioli
        /// </summary>
        ChipExtraction = 2,

        /// <summary>
        /// Esecuzione con spazzolatura trucioli
        /// </summary>
        Brushing = 4
    }
}
