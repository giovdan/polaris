namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Possibili configurazioni dei tipi di motrice dell'impianto
    /// </summary>
    public enum CarriageConfigurationEnum
    {
        PZ = 0,                 // Motrice con pinza orizzontale o verticale
        CAL = 1,                // Motrice con calibro
        PZ_CAL = 2,             // Motrice con pinza o calibro
        PZ_CAL_SOL = 3,         // Motrice con pinza o calibro sollevabile
        PZ_FASCIO = 4,          // Motrice con pinza o fascio
        RULLI = 5,              // Motrice con rulli
        PZ_E_CAL = 6,           // Motrice (AXX) e calibro (BXX) contemporanei
        SPINGITORE = 7,         // Spingitore
        CAL_SPINGI = 8,         // Calibro o Spingitore
        PZ_SPINGI = 9,          // Motrice con pinza o Spingitore
        PZ_FASCIO_SPINGI = 10,  // Motrice con pinza o Fascio o Spingitore fascio
        PZ_VASSOIO = 11         // Motrice con pinza o vassoio (nesting piastre robot)
    }

}
