namespace Mitrol.Framework.Domain.Interfaces
{
    using System;

    public interface IUnitOfWorkFactory<T> : IDisposable
    {
        IUnitOfWork<T> GetOrCreate(IUserSession userSession);
    }

   
}