
namespace Mitrol.Framework.MachineManagement.Data.MySQL.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public class EFEntityRepostiory : BaseRepository<MasterEntity>, IEFEntityRepository
    {
        public EFEntityRepostiory(IServiceFactory serviceFactory
                        , IDatabaseContextFactory databaseContextFactory): base(serviceFactory, databaseContextFactory)
        {

        }

        public MasterEntity Add(MasterEntity entity)
        {
            return UnitOfWork.Context.Entities.Add(entity).Entity;
        }

        public Task<MasterEntity> AddAsync(MasterEntity entity)
        {
            return Task.Factory.StartNew(() => Add(entity));
        }

        public int BatchInsert(IEnumerable<MasterEntity> items)
        {
            UnitOfWork.Context.Entities.AddRange(items);
            return items.Count();
        }

        public int BulkInsert(IEnumerable<MasterEntity> items)
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

        public IEnumerable<MasterEntity> FindBy(Expression<Func<MasterEntity, bool>> predicate)
        {
            return UnitOfWork.Context.Entities.Where(predicate);
        }

        public Task<IEnumerable<MasterEntity>> FindByAsync(Expression<Func<MasterEntity, bool>> predicate)
        {
            return Task.Factory.StartNew(() => FindBy(predicate));
        }

        public MasterEntity Get(long id)
        {
            return UnitOfWork.Context.Entities.SingleOrDefault(e => e.Id == id);
        }

        public IEnumerable<MasterEntity> GetAll()
        {
            return UnitOfWork.Context.Entities;
        }

        public Task<MasterEntity> GetAsync(long id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }

        public void Remove(MasterEntity entity)
        {
            UnitOfWork.Context.Entities.Remove(entity);
        }

        public MasterEntity Update(MasterEntity entity)
        {
            return UnitOfWork.Context.Entities.Update(entity).Entity;
        }

        public MasterEntity RawUpdate(MasterEntity entity)
        {
            UnitOfWork.Context.Database.ExecuteSqlRaw($"UPDATE entity SET DisplayName = '{entity.DisplayName}' WHERE Id = {entity.Id}");
            return UnitOfWork.Context.Entities.SingleOrDefault(e => e.Id == entity.Id);
        }

    }
}
