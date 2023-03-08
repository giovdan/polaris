namespace Mitrol.Framework.Domain.Interfaces
{
    using System;
    using System.Data;

    public interface IUnitOfWorkFactory<T>: IDisposable
    {
        IUnitOfWork<T> GetOrCreate(IUserSession userSession);
    }
}
