namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using AutoMapper;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;

    public class ServiceBase : Disposable, IApplicationService
    {
        protected IServiceFactory ServiceFactory { get; }
        protected IUnitOfWorkFactory UnitOfWorkFactory { get; private set; }
        protected IMapper Mapper { get; set; }
        protected IUserSession UserSession { get; set; }

        public ServiceBase(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
            UnitOfWorkFactory = serviceFactory.GetService<IUnitOfWorkFactory>();
            Mapper = serviceFactory.GetService<IMapper>();
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
        }

        public void SetSession(IUserSession userSession)
        {
            UserSession = userSession;
        }
    }
}