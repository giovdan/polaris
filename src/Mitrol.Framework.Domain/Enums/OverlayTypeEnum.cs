namespace Mitrol.Framework.Domain.Enums
{
    using System;

    [Flags]
    public enum OverlayTypeEnum : int
    {
        /// <summary>
        /// Area pressore (impianti Gemini e TipoG)
        /// </summary>
        PresserArea = 1,

        /// <summary>
        /// Area botola di scarico (impianti TipoG)
        /// </summary>
        HatchArea = 2,

        /// <summary>
        /// Lunghezza barra rimanente (impianti TipoG e Calibro)
        /// </summary>
        RemainingBarLength = 3,

        /// <summary>
        /// Pinza A (impianti TipoG)
        /// </summary>
        PincherA = 4,

        /// <summary>
        /// Pinza B (impianti TipoG)
        /// </summary>
        PincherB = 5,

        /// <summary>
        /// Pinza C (impianti TipoG)
        /// </summary>
        PincherC = 6,

        /// <summary>
        /// Asse ausiliario A (impianti Endeavour ed Excalibur)
        /// </summary>
        AuxiliaryAxisA = 7,

        /// <summary>
        /// Asse ausiliario B (impianti Endeavour ed Excalibur)
        /// </summary>
        AuxiliaryAxisB = 8,

        /// <summary>
        /// Asse ausiliario C (impianti Endeavour ed Excalibur)
        /// </summary>
        AuxiliaryAxisC = 9,

        /// <summary>
        /// Area in testa alla trave di congelamento asse ausiliario (impianti Excalibur)
        /// </summary>
        AuxiliaryAxisFreezingInitialArea = 10,

        /// <summary>
        /// Area in coda alla trave di congelamento asse ausiliario (impianti Excalibur)
        /// </summary>
        AuxiliaryAxisFreezingFinalArea = 11,
    }

}