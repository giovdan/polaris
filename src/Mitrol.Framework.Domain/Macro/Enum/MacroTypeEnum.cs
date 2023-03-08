namespace Mitrol.Framework.Domain.Macro
{
    /// <summary>
    /// Tipi di Macro  
    /// </summary>
    public enum MacroTypeEnum: byte
    {
        NotDefined=0,
        /// <summary>
        /// Macro di taglio
        /// </summary>
        MacroCut = 1,

        /// <summary>
        /// Macro di fresatura
        /// </summary>
        MacroMill=2,

        /// <summary>
        /// Macro Robot
        /// </summary>
        MacroRobot=4,


        MacroAll = MacroCut|MacroMill|MacroRobot,

    }
}
