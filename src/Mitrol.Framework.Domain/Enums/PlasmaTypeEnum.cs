namespace Mitrol.Framework.Domain.Enums
{
    // Configurazione tipo di apparecchiatura plasma (PlasmaH)
    public enum PlasmaTypeEnum
    {
        /// <summary>
        /// Non configurato
        /// </summary>
        NONE = 0,

        /// <summary>
        /// Hypertherm HPR130/260 Consolle Manuale
        /// </summary>
        HPR = 3,

        /// <summary>
        /// Hypertherm HPR130/260 Consolle Automatica
        /// </summary>
        HPRA = 4,

        /// <summary>
        /// Hypertherm HSD130 (solo Manuale)
        /// </summary>
        HSD = 5,

        /// <summary>
        /// Hypertherm PowerMax (Automatico)
        /// </summary>
        POWERMAX = 6,

        /// <summary>
        /// Hypertherm XPR300 Consolle Automatica
        /// </summary>
        XPR = 7
    }
}
