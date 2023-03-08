
namespace RepoDbVsEF.Domain.Models
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Data.RepDb.Interfaces;

    public class RepoDbUnitOfWorkFactory : IUnitOfWorkFactory<IRepoDbDatabaseContext>
    {
        private IUnitOfWork<IRepoDbDatabaseContext> _currentUnitOfWork { get; set; }
        private IServiceFactory _serviceFactory { get; set; }

        public void Dispose()
        {
            _currentUnitOfWork.Dispose();
        }

        public RepoDbUnitOfWorkFactory(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IUnitOfWork<IRepoDbDatabaseContext> GetOrCreate(IUserSession userSession)
        {
            if (_currentUnitOfWork == null)
            {
                _currentUnitOfWork = _serviceFactory.GetService<IUnitOfWork<IRepoDbDatabaseContext>>();
                _currentUnitOfWork.UserSession = userSession;
            }

            return _currentUnitOfWork;
        }
    }
}
