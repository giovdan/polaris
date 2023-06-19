namespace Mitrol.Framework.Domain.Macro
{
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Production.Enums;
    using System.Collections.Generic;
    public class MacroManagement : IMacroManagement
    {
        private IToolManagement _toolManagement;
        public MacroSectionEnum Section { get; private set; }
        public Dictionary<ExternalInterfaceNameEnum, object> Attributes { get; set; }

        public MacroManagement(IToolManagement toolManagement, MacroSectionEnum macroSectionEnum = MacroSectionEnum.ALL)
        {
            Attributes = new Dictionary<ExternalInterfaceNameEnum, object>();
            _toolManagement = toolManagement;
            Section = macroSectionEnum;
        }

        public Result Add(ExternalInterfaceNameEnum externalInterfaceNameEnum, object value)
        {
            if (Attributes.ContainsKey(externalInterfaceNameEnum))
            {
                Attributes.Remove(externalInterfaceNameEnum);
            }
            Attributes.Add(externalInterfaceNameEnum, value);
            return Result.Ok();
        }

        public Result<ExternalBaseData> GetToolAttributes(IToolManagementFilter filter)
        {
            var result= _toolManagement.GetToolAttributes(filter);
            if (result.Failure)
            {
                // In caso di errore il messaggio che mi arriva è già formattato, devo quindi estrapolare l'errore per fornirlo alla DLL 
                var err = result.Error;
                // è stao formattato secondo => "${" + value.ToString() + "}";
                var index=err.IndexOf("{");
                var lastIndex = err.LastIndexOf("}");
                err = err.Substring(index + 1, lastIndex - index - 1);
                return Result.Fail<ExternalBaseData>(err);
            }
            return result;
        }
    }

   
}
