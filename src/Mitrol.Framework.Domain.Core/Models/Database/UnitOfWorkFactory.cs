namespace Mitrol.Framework.Domain.Core.Models
{
    using Microsoft.EntityFrameworkCore.Storage;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System.Threading.Tasks;

    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private IDbContextTransaction _contextTransaction;
        private readonly IServiceFactory _serviceFactory;

        public UnitOfWorkFactory(IServiceFactory serviceFactory)
        {
            _contextTransaction = null;
            _serviceFactory = serviceFactory;
        }

        public IUnitOfWork Create()
        {
            return Create(NullUserSession.Instance);
        }

        public IUnitOfWork Create(IUserSession session)
        {
            var uow = _serviceFactory.GetService<IUnitOfWork>();
            uow.Session = session ?? NullUserSession.Instance;
            return uow;
        }

        public IDbContextTransaction BeginTransaction(IUnitOfWork unitOfWork)
        {
            _contextTransaction = unitOfWork.Context.Database.BeginTransaction();
            return _contextTransaction;
        }

        public void RollBackTransaction(IUnitOfWork unitOfWork)
        {
            if (_contextTransaction != null)
                unitOfWork.Context.Database.RollbackTransaction();
        }

        public void CommitTransaction(IUnitOfWork unitOfWork)
        {
            if (_contextTransaction != null)
                unitOfWork.Context.Database.CommitTransaction();
        }

        /// <summary>
        /// Remove UnitOfWork
        /// </summary>
        /// <param name="unitOfWork"></param>
        public int Commit(IUnitOfWork unitOfWork)
        {
            return unitOfWork.Commit();
        }

        public Task<int> CommitAsync(IUnitOfWork unitOfWork)
        {
            return unitOfWork.CommitAsync();
        }

    }
}