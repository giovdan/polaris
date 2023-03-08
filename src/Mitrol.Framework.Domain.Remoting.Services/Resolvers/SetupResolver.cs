namespace Mitrol.Framework.RemoteProcessor
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Remoting.Services;

    public class ProcessingSetupResolver : IResolver<IProcessingSetup>
    {
        private readonly IServiceFactory _serviceFactory;

        public ProcessingSetupResolver(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IProcessingSetup Resolve()
        {
            return _serviceFactory.GetService<RemoteProcessingSetup>();
        }

        object IResolver.Resolve() => Resolve();
    }
}