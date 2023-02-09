
namespace RepoDbVsEF.EF.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Data;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.EF.Data.Interfaces;

    public class EFUnitOfWorkFactory : IUnitOfWorkFactory<IEFDatabaseContext>
    {
        private readonly IServiceFactory _serviceFactory;
        private Dictionary<string, IUnitOfWork<IEFDatabaseContext>> _unitOfWorks;

        public EFUnitOfWorkFactory(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _unitOfWorks = new Dictionary<string, IUnitOfWork<IEFDatabaseContext>>();
        }

        public IUnitOfWork<IEFDatabaseContext> Create(IUserSession session)
        {
            var uow = _serviceFactory.GetService<IUnitOfWork<IEFDatabaseContext>>();
            uow.UserSession = session;
            _unitOfWorks.TryAdd(uow.Id, uow);
            return uow;
        }

        public void BeginTransaction(IUnitOfWork<IEFDatabaseContext> unitOfWork
                , IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            var transaction = unitOfWork.Context.Database.BeginTransaction(isolationLevel);
            unitOfWork.CurrentTransaction = transaction as IDbTransaction;
        }

        public void Commit(IUnitOfWork<IEFDatabaseContext> unitOfWork)
        {
            if (_unitOfWorks.ContainsKey(unitOfWork.Id))
            {
                unitOfWork.Commit();
            }
        }

        public void CommitTransaction(IUnitOfWork<IEFDatabaseContext> unitOfWork)
        {
            if (_unitOfWorks.ContainsKey(unitOfWork.Id))
            {
                unitOfWork.Context.Database.CommitTransaction();
            }
        }

        public IUnitOfWork<IEFDatabaseContext> Create()
        {
            return Create(null);
        }

        public void RollBackTransaction(IUnitOfWork<IEFDatabaseContext> unitOfWork)
        {
            if (_unitOfWorks.ContainsKey(unitOfWork.Id))
            {
                unitOfWork.Context.Database.RollbackTransaction();
            }

        }

        public void Dispose()
        {
            foreach (var unitOfWork in _unitOfWorks)
            {
                unitOfWork.Value.Dispose();
            }

            _unitOfWorks.Clear();
        }
    }
}
