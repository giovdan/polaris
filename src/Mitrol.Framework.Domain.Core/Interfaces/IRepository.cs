
namespace Mitrol.Framework.Domain.Core.Interfaces
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IReadOnlyRepository<TEntity, TConnection> where TEntity : BaseEntity
    {
        void Attach(IUnitOfWork<TConnection> unitOfWork);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> FindBy<TKey>(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, TKey>> orderBy);
        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(long id);
        IEnumerable<TEntity> GetAll();
        Task<TEntity> GetAsync(long id);
    }

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