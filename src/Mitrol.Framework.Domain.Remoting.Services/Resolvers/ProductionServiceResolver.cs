namespace Mitrol.Framework.RemoteProcessor
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Production.Interfaces;
    using Mitrol.Framework.Domain.Remoting.Services;

    public class ProductionServiceResolver : IResolver<IRemoteProductionService>
    {
        private readonly IServiceFactory _serviceFactory;

        public ProductionServiceResolver(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IRemoteProductionService Resolve()
        {
            return _serviceFactory.GetService<RemoteProductionService>();
        }

        object IResolver.Resolve() => Resolve();
    }
}