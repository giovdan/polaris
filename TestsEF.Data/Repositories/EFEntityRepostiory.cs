
namespace RepoDbVsEF.EF.Data.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.Domain.Models;
    using RepoDbVsEF.EF.Data.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public class EFEntityRepostiory : BaseRepository<DatabaseEntity>, IEFEntityRepository
    {
        public EFEntityRepostiory(IServiceFactory serviceFactory
                        , IDatabaseContextFactory databaseContextFactory): base(serviceFactory, databaseContextFactory)
        {

        }

        public DatabaseEntity Add(DatabaseEntity entity)
        {
            return UnitOfWork.Context.Entities.Add(entity).Entity;
        }

        public Task<DatabaseEntity> AddAsync(DatabaseEntity entity)
        {
            return Task.Factory.StartNew(() => Add(entity));
        }

        public int BatchInsert(IEnumerable<DatabaseEntity> items)
        {
            UnitOfWork.Context.Entities.AddRange(items);
            return items.Count();
        }

        public int BulkInsert(IEnumerable<DatabaseEntity> items)
        {
            StringBuilder insertQuery = new($"REPLACE INTO `Entity` (`Code`, `EntityTypeId``, `RowVersion`) VALUES ");

            foreach (var item in items)
            {
                insertQuery.Append($"('{item.Code}',{item.EntityTypeId},'{item.RowVersion}'),");
            }

            insertQuery.Length -= 1;

            var result = UnitOfWork.Context.Database.ExecuteSqlRaw(insertQuery.ToString());
            return result;
        }

        public IEnumerable<DatabaseEntity> FindBy(Expression<Func<DatabaseEntity, bool>> predicate)
        {
            return UnitOfWork.Context.Entities.Where(predicate);
        }

        public Task<IEnumerable<DatabaseEntity>> FindByAsync(Expression<Func<DatabaseEntity, bool>> predicate)
        {
            return Task.Factory.StartNew(() => FindBy(predicate));
        }

        public DatabaseEntity Get(ulong id)
        {
            return UnitOfWork.Context.Entities.SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<DatabaseEntity> GetAll()
        {
            return UnitOfWork.Context.Entities;
        }

        public Task<DatabaseEntity> GetAsync(ulong id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }

        public void Remove(DatabaseEntity entity)
        {
            UnitOfWork.Context.Entities.Remove(entity);
        }

        public DatabaseEntity Update(DatabaseEntity entity)
        {
            return UnitOfWork.Context.Entities.Update(entity).Entity;
        }

        public DatabaseEntity RawUpdate(DatabaseEntity entity)
        {
            UnitOfWork.Context.Database.ExecuteSqlRaw($"UPDATE entity SET DisplayName = '{entity.DisplayName}' WHERE Id = {entity.Id}");
            return UnitOfWork.Context.Entities.SingleOrDefault(e => e.Id == entity.Id);
        }

    }
}
