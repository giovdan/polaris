namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using AutoMapper;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;

    public class BaseService : Disposable, IApplicationService
    {
        protected IServiceFactory ServiceFactory { get; }
        protected IUnitOfWorkFactory<IEFDatabaseContext> UnitOfWorkFactory { get; private set; }
        protected IMapper Mapper { get; set; }
        protected IUserSession UserSession { get; set; }
        public IEntityRepository EntityRepository => ServiceFactory.GetService<IEntityRepository>();

        public BaseService(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
            UnitOfWorkFactory = serviceFactory.GetService<IUnitOfWorkFactory<IEFDatabaseContext>>();
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