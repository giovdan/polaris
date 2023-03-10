namespace Mitrol.Framework.MachineManagement.Application.Core
{
    using AutoMapper;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces;

    public class BaseService : Disposable, IApplicationService
    {
        protected IServiceFactory ServiceFactory { get; }
        protected IUnitOfWorkFactory<IEFDatabaseContext> UnitOfWorkFactory { get; private set; }
        protected IMapper Mapper { get; set; }
        protected IUserSession UserSession { get; set; }

        public BaseService(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
            UnitOfWorkFactory = ServiceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>();
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
