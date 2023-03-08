namespace Mitrol.Framework.RemoteProcessor
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Production.Interfaces;
    using Mitrol.Framework.Domain.Remoting.Services;

    public class ToolServiceResolver : IResolver<IRemoteToolService>
    {
        private readonly IServiceFactory _serviceFactory;

        public ToolServiceResolver(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IRemoteToolService Resolve()
        {
            return _serviceFactory.GetService<RemoteToolService>();
        }

        object IResolver.Resolve() => Resolve();
    }
}