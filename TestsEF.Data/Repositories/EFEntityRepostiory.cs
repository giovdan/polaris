
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

    public class EFEntityRepostiory : BaseRepository<Entity>, IEFEntityRepository
    {
        public EFEntityRepostiory(IServiceFactory serviceFactory
                        , IDatabaseContextFactory databaseContextFactory): base(serviceFactory, databaseContextFactory)
        {

        }

        public Entity Add(Entity entity)
        {
            return UnitOfWork.Context.Entities.Add(entity).Entity;
        }

        public Task<Entity> AddAsync(Entity entity)
        {
            return Task.Factory.StartNew(() => Add(entity));
        }

        public int BatchInsert(IEnumerable<Entity> items)
        {
            UnitOfWork.Context.Entities.AddRange(items);
            return items.Count();
        }

        public int BulkInsert(IEnumerable<Entity> items)
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

        public IEnumerable<Entity> FindBy(Expression<Func<Entity, bool>> predicate)
        {
            return UnitOfWork.Context.Entities.Where(predicate);
        }

        public Task<IEnumerable<Entity>> FindByAsync(Expression<Func<Entity, bool>> predicate)
        {
            return Task.Factory.StartNew(() => FindBy(predicate));
        }

        public Entity Get(ulong id)
        {
            return UnitOfWork.Context.Entities.SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<Entity> GetAll()
        {
            return UnitOfWork.Context.Entities;
        }

        public Task<Entity> GetAsync(ulong id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }

        public void Remove(Entity entity)
        {
            UnitOfWork.Context.Entities.Remove(entity);
        }

        public Entity Update(Entity entity)
        {
            return UnitOfWork.Context.Entities.Update(entity).Entity;
        }

        public void RawUpdate(Entity entity)
        {
            UnitOfWork.Context.Database.ExecuteSqlRaw($"UPDATE entity SET DisplayName = '{entity.DisplayName}' WHERE Id = {entity.Id}");
        }

    }
}
