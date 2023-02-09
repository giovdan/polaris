namespace RepoDbVsEF.Domain.Interfaces
{
    using System;
    using System.Data;

    public interface IUnitOfWorkFactory<T>: IDisposable
    {
        IUnitOfWork<T> Create();
        void Commit(IUnitOfWork<T> unitOfWork);
        void RollBackTransaction(IUnitOfWork<T> unitOfWork);
        void CommitTransaction(IUnitOfWork<T> unitOfWork);
        void BeginTransaction(IUnitOfWork<T> unitOfWork, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
    }
}
