
namespace RepoDbVsEF.EF.Data.Models
{
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.EF.Data.Interfaces;
    using System.Collections.Generic;

    public class EFUnitOfWorkFactory : IUnitOfWorkFactory<IEFDatabaseContext>
    {
        private readonly IServiceFactory _serviceFactory;
        //private Dictionary<string, IUnitOfWork<IEFDatabaseContext>> _unitOfWorks;
        private IUnitOfWork<IEFDatabaseContext> _currentUnitOfWork;

        public EFUnitOfWorkFactory(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IUnitOfWork<IEFDatabaseContext> GetOrCreate(IUserSession session)
        {
            if (_currentUnitOfWork == null)
            {
                _currentUnitOfWork = _serviceFactory.GetService<IUnitOfWork<IEFDatabaseContext>>();
                _currentUnitOfWork.UserSession = session;
            }
            
            return _currentUnitOfWork;
        }

        public void Dispose()
        {
            _currentUnitOfWork?.Dispose();
        }
    }
}
