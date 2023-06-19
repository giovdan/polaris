namespace Mitrol.Framework.Domain.Macro
{
    /// <summary>
    /// Enumerativo corrispondente al tipo di operazione da eseguire (vecchio protocollo LeadCut..)
    /// </summary>
    public enum MacroOperationTypeEnum
    {
        NotDefined = 0,

        /// <summary>
        /// Operazione di "LEAD"
        /// </summary>
        Lead = 1,

        /// <summary>
        /// Operazione di "CUT"
        /// </summary>
        Cut = 2,

        /// <summary>
        /// Operazione di "HOL"
        /// </summary>
        Hol = 3,

        /// <summary>
        /// Operazione di "PALP"
        /// </summary>
        Palp = 4,

        /// <summary>
        /// Operazione di "MARK"
        /// </summary>
        Mark = 5,
    }
}
