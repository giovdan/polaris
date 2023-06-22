namespace Mitrol.Framework.MachineManagement.Application.Resolvers
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using Mitrol.Framework.MachineManagement.Application.RulesHandlers;

    public class ToolRulesHandlerResolver : IResolver<IEntityRulesHandler<ToolDetailItem>>
    {
        private readonly IServiceFactory _serviceFactory;

        public ToolRulesHandlerResolver(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        object IResolver.Resolve()
        {
            return Resolve();
        }

        public IEntityRulesHandler<ToolDetailItem> Resolve()
        {
            return _serviceFactory.GetService<ToolRulesHandler>();
        }
    }
}
