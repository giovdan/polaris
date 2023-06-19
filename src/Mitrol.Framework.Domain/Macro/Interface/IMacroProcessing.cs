namespace Mitrol.Framework.Domain.Macro
{
    using Mitrol.Framework.Domain.Configuration.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Production.Enums;

    /// <summary>
    /// Interfaccia da implementare per poter essere utilizzata nel caricamento dinamico della DLL che risolve le Macro 
    /// </summary>
    public interface IMacroProcessing
    {

        /// <summary>
        /// Interfaccia per l'accesso ai dati di configurazione della macchina
        /// </summary>
        IRemoteMachineConfigurationService MachineConfigurationService { get; }

        /// <summary>
        /// Interfaccia per l'accesso ai parametri della macchina
        /// </summary>
        IRemoteMachineParameterService MachineParameterService { get; }

        /// <summary>
        /// Tipo di Macro viene implementata dall'istanza di questa interfaccia  
        /// </summary>
        MacroTypeEnum MacroType { get; } 

        string DllInformation { get; }

        /// <summary>
        /// Metodo per l'elaborazione dell'operazione di Macro
        /// </summary>
        /// <param name="macroData"></param>
        /// <returns>Viene restituito un oggetto contenente la lista di operazioni elaborate</returns>
        Result<MacroOutputData> ProcessMacroOperation(IMacroManagement macroData);

    }

    /// <summary>
    /// Interfaccia dei dati passati alla libreria delle Macro.
    /// </summary>
    public interface IMacroManagement:IDataExternalInterface,IToolManagement
    {
        MacroSectionEnum Section { get; }
    }
}
