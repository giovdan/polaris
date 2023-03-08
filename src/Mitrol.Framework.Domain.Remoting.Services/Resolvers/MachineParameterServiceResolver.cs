namespace Mitrol.Framework.RemoteProcessor
{
    using Mitrol.Framework.Domain.Interfaces;

    public class MachineParameterServiceResolver : IResolver<IRemoteMachineParameterService>
    {
        private readonly IServiceFactory _serviceFactory;

        public MachineParameterServiceResolver(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IRemoteMachineParameterService Resolve()
        {
            return _serviceFactory.GetService<IRemoteMachineParameterService>();
        }

        object IResolver.Resolve() => Resolve();
    }
}