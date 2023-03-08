namespace Mitrol.Framework.Domain.Configuration
{
    public enum GFunctionEnum
    {
        /// <summary>
        /// Abilitazione Palpatura Foratura / Fresatura con dispositivo laser
        /// </summary>
        G220,

        /// <summary>
        /// Abilitazione funzione G228 (palpatura con dispositivo meccanico)
        /// </summary>
        G228,

        /// <summary>
        /// Abilitazione Palpatura Foratura / Fresatura con utensile speciale di palpatura TS70
        /// </summary>
        G229,

        /// <summary>
        /// Flag di abilitazione funzione G256 (anticipo cambio utensile)
        /// </summary>
        G256,

        /// <summary>
        /// Flag di abilitazione funzione G250 (eliminazione trucioli) in modalità Spazzolatura 
        /// </summary>
        G250_1,

        /// <summary>
        /// Flag di abilitazione funzione G250 (eliminazione trucioli) in modalità Aspirazione trucioli
        /// </summary>
        G250_2,

        /// <summary>
        /// Flag di abilitazione funzione G250 (eliminazione trucioli) in modalità Aspirazione + Spazzolatura 
        /// </summary>
        G250_3,

        /// <summary>
        /// Flag di abilitazione funzione G270 (Ottimizzazione DNC foratura)
        /// </summary>
        G270,

        /// <summary>
        /// Flag di abilitazione funzione G281 (fine stazione per tandem)
        /// </summary>
        G281,

        /// <summary>
        /// Flag di abilitazione funzione G292 (Ottimizzazione DNC tagli plasma)
        /// </summary>
        G292,

        /// <summary>
        /// Flag di abilitazione funzione G294 (Ottimizzazione DNC tagli ossitaglio)
        /// </summary>
        G294,
    }
}