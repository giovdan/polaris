﻿

namespace RepoDbVsEF.Domain.Interfaces
{
    using RepoDbVsEF.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public interface IRepository<TEntity,TConnection>
        where TEntity : BaseEntity
    {
        void Attach(IUnitOfWork<TConnection> unitOfWork);

        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);


        Task<IEnumerable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate);

        TEntity Get(ulong id);

        IEnumerable<TEntity> GetAll();

        Task<TEntity> GetAsync(ulong id);

        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        void Remove(TEntity entity);

        TEntity Update(TEntity entity);

        int BatchInsert(IEnumerable<TEntity> items);

        int BulkInsert(IEnumerable<TEntity> items);
    }
}