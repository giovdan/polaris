namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Enums;
    using System.Collections.Generic;

    /// <summary>
    /// Interface IAttributeHandlersResolver: Get every handlers related to specified parent type 
    /// </summary>
    public interface IEntityHandlerFactory
    {
        IEnumerable<IEntityRulesHandler> ResolveHandlers(ParentTypeEnum parentType);
    }

   

}
