namespace Mitrol.Framework.MachineManagement.Data.MySQL.Models
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;

    public class UnitOfWorkFactory : IUnitOfWorkFactory<IMachineManagentDatabaseContext>
    {
        private readonly IServiceFactory _serviceFactory;
        //private Dictionary<string, IUnitOfWork<IEFDatabaseContext>> _unitOfWorks;
        private IUnitOfWork<IMachineManagentDatabaseContext> _currentUnitOfWork;

        public UnitOfWorkFactory(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IUnitOfWork<IMachineManagentDatabaseContext> GetOrCreate(IUserSession session)
        {
            if (_currentUnitOfWork == null)
            {
                _currentUnitOfWork = _serviceFactory.GetService<IUnitOfWork<IMachineManagentDatabaseContext>>();
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
