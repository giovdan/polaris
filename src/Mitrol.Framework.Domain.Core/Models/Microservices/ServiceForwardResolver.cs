namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Interfaces;

    public class ServiceForwardResolver<TContract, TService>
        : ServiceForward<TContract, TService>
        , IResolver<TContract> where TService : class, TContract
        
    {
        public ServiceForwardResolver(IServiceFactory serviceFactory)
            : base(serviceFactory) { }

        public override TContract Resolve()
        {
            return ServiceFactory.Resolve<TService>();
        }
    }
}
