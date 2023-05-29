namespace Mitrol.Framework.MachineManagement.Data.MySQL.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public class EntityLinkRepository : BaseRepository<EntityLink>, IEntityLinkRepository
    {
        protected IServiceFactory ServiceFactory { get; private set; }

        public EntityLinkRepository(IServiceFactory serviceFactory, IDatabaseContextFactory databaseContextFactory) : base(serviceFactory, databaseContextFactory)
        {

        }

        public int BatchInsert(IEnumerable<EntityLink> EntityLinks)
        {
            UnitOfWork.Context.EntityLinks.AddRange(EntityLinks);
            return EntityLinks.Count();
        }

        public int BulkInsert(IEnumerable<EntityLink> EntityLinks)
        {
            StringBuilder insertQuery = new($"INSERT INTO `EntityLink` (`ParentId`, `ChildId`, `Level`, `RowNumber`) VALUES ");

            foreach (var item in EntityLinks)
            {
                insertQuery.Append($"({item.EntityId},{item.RelatedEntityId},{item.Level},{item.RowNumber}");
            }

            insertQuery.Length -= 1;

            var result = UnitOfWork.Context.Database.ExecuteSqlRaw(insertQuery.ToString());
            return result;

        }

        public IEnumerable<EntityLink> FindBy(Expression<System.Func<EntityLink, bool>> predicate)
        {
            return UnitOfWork.Context.EntityLinks.Where(predicate);
        }

        public Task<IEnumerable<EntityLink>> FindByAsync(Expression<System.Func<EntityLink, bool>> predicate)
        {
            return new TaskFactory().StartNew(() => FindBy(predicate));
        }

        public EntityLink Get(long id)
        {
            return UnitOfWork.Context.EntityLinks.SingleOrDefault(child => child.Id == id);
        }

        public IEnumerable<EntityLink> GetAll()
        {
            return UnitOfWork.Context.EntityLinks.AsEnumerable();
        }

        public Task<EntityLink> GetAsync(long id)
        {
            return new TaskFactory().StartNew(() => Get(id));
        }

        public EntityLink Add(EntityLink entity)
        {
            return UnitOfWork.Context.EntityLinks.Add(entity).Entity;
        }

        public Task<EntityLink> AddAsync(EntityLink entity)
        {
            return new TaskFactory().StartNew(() => Add(entity));
        }

        public void Remove(EntityLink entity)
        {
            UnitOfWork.Context.EntityLinks.Remove(entity);
        }

        public EntityLink Update(EntityLink entity)
        {
            UnitOfWork.Context.EntityLinks.Update(entity);
            return entity;
        }

        public void RemoveChildLinks(long parentId)
        {
            UnitOfWork.Context.Database.ExecuteSqlRaw($"DELETE FROM INTO `EntityLink` WHERE EntityId = {parentId} AND ");
        }

    }
}
