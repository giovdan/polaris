namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Enumerato che definisce il tipo di taglio selezionato
    /// </summary>
    public enum CuttingTypeEnum
    {
        CUT_CESO    = 0,   // Cesoia
        CUT_SEGA    = 1,   // Segatrice
        CUT_OXY     = 2,   // Ossitaglio
        CUT_PLA     = 3,   // Plasma
        CUT_CESO_P  = 4,   // Cesoia per Piatti
        CUT_TVB     = 5,   // Segatrice tipo TVB
        CUT_STZ     = 6,   // Separazione dei pezzi con stozzatrice
        CUT_PLA_HT  = 8,   // Non utilizzato (ex HT4400)
        CUT_POLIF2  = 9,   // Seconda unità polifunzionale
    }
}
