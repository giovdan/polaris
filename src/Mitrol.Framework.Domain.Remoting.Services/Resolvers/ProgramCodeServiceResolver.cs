namespace Mitrol.Framework.RemoteProcessor
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Remoting.Services;

    public class ProgramCodeServiceResolver : IResolver<IRemoteProgramCodeService>
    {
        private readonly IServiceFactory _serviceFactory;

        public ProgramCodeServiceResolver(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IRemoteProgramCodeService Resolve()
        {
            return _serviceFactory.GetService<RemoteProgramCodeService>();
        }

        object IResolver.Resolve() => Resolve();
    }
}