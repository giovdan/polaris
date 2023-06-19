namespace Mitrol.Framework.Domain.Macro
{
    using Mitrol.Framework.Domain.Production.Models;

    /// <summary>
    /// Oggetto contenente i dati relativi della Macro (Nome, A , B...)
    /// </summary>
    public class MacroParameters : MacroItemAttributes
    {
        /// <summary>
        /// Construttore
        /// </summary>
        /// <param name="macrotype"></param>
        public MacroParameters(MacroTypeEnum macrotype)
            :base(macrotype)
        {
        }

        /// <summary>
        /// Funzione per aggiunta veloce degli attributi da oggetto PieceOperationItem 
        /// </summary>
        /// <param name="item"></param>
        public void AddData(PieceOperationItem item)
        {
            Attributes = item.ConvertToExternalInterfaceDictionary(ExternalInterfaceNameEnum.Macro, macroTypeEnum);
        }
    }
}
