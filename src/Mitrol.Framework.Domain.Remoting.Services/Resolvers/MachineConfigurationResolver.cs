namespace Mitrol.Framework.RemoteProcessor
{
    using Mitrol.Framework.Domain.Configuration.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;

    public class MachineConfigurationResolver : IResolver<IRemoteMachineConfigurationService>
    {
        private readonly IServiceFactory _serviceFactory;

        public MachineConfigurationResolver(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IRemoteMachineConfigurationService Resolve()
        {
            return _serviceFactory.GetService<IRemoteMachineConfigurationService>();
        }

        object IResolver.Resolve() => Resolve();
    }
}