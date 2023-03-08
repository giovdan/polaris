namespace Mitrol.Framework.RemoteProcessor
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Production.Interfaces;
    using Mitrol.Framework.Domain.Remoting.Services;

    public class ToolRangeServiceResolver : IResolver<IRemoteToolRangeService>
    {
        private readonly IServiceFactory _serviceFactory;

        public ToolRangeServiceResolver(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IRemoteToolRangeService Resolve()
        {
            return _serviceFactory.GetService<RemoteToolRangeService>();
        }

        object IResolver.Resolve() => Resolve();
    }
}