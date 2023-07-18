namespace Mitrol.Framework.MachineManagement.Application.RulesHandlers
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using System.Collections.Generic;

    public class BaseEntityRulesHandler
    {
        protected Dictionary<AttributeDefinitionEnum, object> AdditionalInfo { get; set; }
        public IServiceFactory ServiceFactory { get; private set; }

        public BaseEntityRulesHandler(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
        }

        public void Init(Dictionary<AttributeDefinitionEnum, object> additionalInfo)
        {
            AdditionalInfo = additionalInfo;
        }
    }
}
