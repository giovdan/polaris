namespace RepoDbVsEF.Application.Core
{
    using AutoMapper;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.Domain.Models;
    using RepoDbVsEF.EF.Data.Interfaces;

    public class BaseService : Disposable, IApplicationService
    {
        protected IServiceFactory ServiceFactory { get; }
        protected IUnitOfWorkFactory<IUnitOfWork<IEFDatabaseContext>> UnitOfWorkFactory { get; private set; }
        protected IMapper Mapper { get; set; }
        protected IUserSession UserSession { get; set; }

        public BaseService(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
            UnitOfWorkFactory = serviceFactory.GetService<IUnitOfWorkFactory<IUnitOfWork<IEFDatabaseContext>>>();
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
