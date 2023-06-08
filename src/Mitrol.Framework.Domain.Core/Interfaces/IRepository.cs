
namespace Mitrol.Framework.Domain.Core.Interfaces
{
    using Mitrol.Framework.Domain.Core.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<TEntity, TConnection>: IReadOnlyRepository<TEntity, TConnection>
            where TEntity : BaseEntity
    {

        TEntity Add(TEntity entity);
        Task<TEntity> AddAsync(TEntity entity);
        void Remove(TEntity entity);
        TEntity Update(TEntity entity);
        int BatchInsert(IEnumerable<TEntity> items);
        int BulkInsert(IEnumerable<TEntity> items);
    }
}