namespace Mitrol.Framework.MachineManagement.Data.MySQL.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Models.Views;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public class EntityRepostiory : BaseRepository<Entity>, IEntityRepository
    {
        public EntityRepostiory(IServiceFactory serviceFactory
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
            StringBuilder insertQuery = new($"REPLACE INTO `Entity` (`DisplayName`, `EntityTypeId`,`SecondaryKey`,`HashCode`,`Status`, `Priority`, `RowVersion`) VALUES ");

            foreach (var item in items)
            {
                insertQuery.Append($"('{item.DisplayName}',{item.EntityTypeId},{item.SecondaryKey},{item.HashCode},'{item.Status}',{item.Priority}'{item.RowVersion}'),");
            }

            insertQuery.Length -= 1;

            var result = UnitOfWork.Context.Database.ExecuteSqlRaw(insertQuery.ToString());
            return result;
        }

        public IEnumerable<Entity> FindBy(Expression<Func<Entity, bool>> predicate)
        {
            var query = UnitOfWork.Context.Entities.Where(predicate);

            Debug.WriteLine(query.ToQueryString());

            return query;
        }

        public Task<IEnumerable<Entity>> FindByAsync(Expression<Func<Entity, bool>> predicate) => Task.Factory.StartNew(() => FindBy(predicate));

        public Entity Get(long id) => UnitOfWork.Context.Entities
            .Include(e => e.EntityType)
            .SingleOrDefault(e => e.Id == id);

        public Entity Get(string displayName) => UnitOfWork.Context.Entities
            .Include(e => e.EntityType)
            .SingleOrDefault(e => e.DisplayName == displayName);

        public Entity GetBySecondaryKey(long secondaryKey, EntityTypeEnum entitType) 
                    => UnitOfWork.Context.Entities.Include(e => e.EntityType)
                            .SingleOrDefault(e => e.SecondaryKey == secondaryKey && e.EntityTypeId == entitType);

        public IEnumerable<Entity> GetAll() => UnitOfWork.Context.Entities
                                            .Include(e => e.EntityType);

        public Task<Entity> GetAsync(long id) => Task.Factory.StartNew(() => Get(id));

        public void Remove(Entity entity)
        {
            UnitOfWork.Context.Entities.Remove(entity);
        }

        public Entity Update(Entity entity) => UnitOfWork.Context.Entities.Update(entity).Entity;

        public Entity RawUpdate(Entity entity)
        {
            UnitOfWork.Context.Database.ExecuteSqlRaw($"UPDATE entity SET DisplayName = '{entity.DisplayName}' WHERE Id = {entity.Id}");
            return UnitOfWork.Context.Entities.SingleOrDefault(e => e.Id == entity.Id);
        }

        public IEnumerable<Entity> FindBy<TKey>(Expression<Func<Entity, bool>> predicate, Expression<Func<Entity, TKey>> orderBy)
                => UnitOfWork.Context.Entities.Where(predicate)
                        .OrderBy(orderBy);

        public IEnumerable<EntityStatusAttribute> GetStatusAttributes(Func<EntityStatusAttribute, bool> predicate)
            => UnitOfWork.Context.EntityStatusAttributes.Where(predicate);

        #region < Tools Management >
        public IEnumerable<PlasmaToolMaster> FindPlasmaToolMasters(Expression<Func<PlasmaToolMaster, bool>> predicate)
        {
            return UnitOfWork.Context.PlasmaToolMasters.Where(predicate);
        }

        public IEnumerable<Tool> FindTools(Expression<Func<Tool,bool>> predicate)
        {
            return UnitOfWork.Context.Tools.Where(predicate);
        }


        #endregion
    }
}
