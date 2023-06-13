namespace Mitrol.Framework.Domain.Interfaces
{
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Mitrol.Framework.Domain.Interfaces;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    public interface IDatabaseContext : IDisposable
    {
        int SaveChanges();

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));

        EntityEntry Entry(object entity);

        void SetSession(IUserSession session);

        DatabaseFacade Database { get; }
    }
}