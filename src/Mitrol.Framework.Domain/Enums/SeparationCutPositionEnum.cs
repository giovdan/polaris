namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Posizione del taglio rispetto a filo fisso e filo mobile
    /// </summary>
    public enum  SeparationCutPositionEnum
    {
        /// <summary>
        /// Taglio che parte sul filo fisso
        /// </summary>
        CutOnFF = 1,
        
        /// <summary>
        /// Taglio solitamente presente nelle macro MSaw che non parte né dal filo fisso né da quello mobile 
        /// </summary>
        CutOnMiddle = 2,

        /// <summary>
        /// Taglio solitamente presente nelle macro MSaw che parte sul filo mobile
        /// </summary>
        CutOnMM = 3,
    }
}
