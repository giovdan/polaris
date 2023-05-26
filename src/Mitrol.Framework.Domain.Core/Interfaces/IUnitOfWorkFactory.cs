namespace Mitrol.Framework.Domain.Core.Interfaces
{
    using Mitrol.Framework.Domain.Interfaces;
    using System;

    public interface IUnitOfWorkFactory<T> : IDisposable
    {
        IUnitOfWork<T> GetOrCreate(IUserSession userSession);
    }

   
}