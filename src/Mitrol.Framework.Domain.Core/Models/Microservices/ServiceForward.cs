namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Interfaces;

    public class ServiceForward<TContract, TService> : IResolver<TContract> where TService : class, TContract
    {
        protected IServiceFactory ServiceFactory { get; }
        
        public ServiceForward(IServiceFactory serviceFactory)
        {
            this.ServiceFactory = serviceFactory;
        }

        public virtual TContract Resolve()
        {
            return ServiceFactory.GetService<TService>();
        }

        object IResolver.Resolve() => Resolve();
    }
}
