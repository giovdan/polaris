
namespace RepoDbVsEF.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using RepoDbVsEF.Data.Interfaces;
    using RepoDbVsEF.Domain.Interfaces;

    public class RepoDbUnitOfWorkFactory : IUnitOfWorkFactory<IRepoDbDatabaseContext>
    {
        private IDbTransaction _transaction;
        private readonly IServiceFactory _serviceFactory;
        private Dictionary<string, IUnitOfWork<IRepoDbDatabaseContext>> _unitOfWorks;

        public RepoDbUnitOfWorkFactory(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            _unitOfWorks = new Dictionary<string, IUnitOfWork<IRepoDbDatabaseContext>>();
        }

        public void BeginTransaction(IUnitOfWork<IRepoDbDatabaseContext> unitOfWork, IsolationLevel isolationLevel)
        {
        if (unitOfWork.Context == null)
            {
                throw new InvalidOperationException("Context not defined");
            }

            if (unitOfWork.Context.Connection == null)
            {
                throw new InvalidOperationException("Connection not defined");
            }

            if (_transaction != null)
            {
                throw new InvalidOperationException("Cannot start a new transaction while the existing one is still open");
            }

            if (unitOfWork.Context.Connection.State == ConnectionState.Closed)
            {
                unitOfWork.Context.Connection.Open();
            }

            _transaction = unitOfWork.Context.Connection.BeginTransaction(isolationLevel);
            unitOfWork.CurrentTransaction = _transaction;
        }

        public void Commit(IUnitOfWork<IRepoDbDatabaseContext> uow)
        {
            CommitTransaction(uow);
        }

        public IUnitOfWork<IRepoDbDatabaseContext> Create()
        {
            var uow = _serviceFactory.GetService<IUnitOfWork<IRepoDbDatabaseContext>>();
            uow.Context = _serviceFactory.GetService<IRepoDbDatabaseContext>();
            _unitOfWorks.TryAdd(uow.Id, uow);
            return uow;
        }

        public void RollBackTransaction(IUnitOfWork<IRepoDbDatabaseContext> unitOfWork)
        {
            if (_unitOfWorks.ContainsKey(unitOfWork.Id))
            {
                _transaction.Rollback();
                unitOfWork.Context.Connection.Close();
                _unitOfWorks.Remove(unitOfWork.Id);
            }
        }

        public void CommitTransaction(IUnitOfWork<IRepoDbDatabaseContext> unitOfWork)
        {
            if (_unitOfWorks.ContainsKey(unitOfWork.Id))
            {
                _transaction.Commit();
                unitOfWork.Context.Connection.Close();
                _unitOfWorks.Remove(unitOfWork.Id);
                unitOfWork.Dispose();
                unitOfWork = null;
            }
        }

        public void Dispose()
        {
            _transaction?.Dispose();
            _transaction = null;
            foreach(var unitOfWork in _unitOfWorks)
            {
                if (unitOfWork.Value.Context.Connection.State == ConnectionState.Open)
                {
                    unitOfWork.Value.Context.Connection.Close();
                }
                unitOfWork.Value.Context.Connection.Dispose();
            }
        }
    }
}
