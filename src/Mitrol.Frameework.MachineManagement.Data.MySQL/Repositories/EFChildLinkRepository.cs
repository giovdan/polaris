namespace Mitrol.Framework.MachineManagement.Data.MySQL.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public class EFLinkRepository : BaseRepository<Link>, ILinkRepository
    {
        protected IServiceFactory ServiceFactory { get; private set; }

        public EFLinkRepository(IServiceFactory serviceFactory, IDatabaseContextFactory databaseContextFactory) : base(serviceFactory, databaseContextFactory)
        {

        }

        public int BatchInsert(IEnumerable<Link> Links)
        {
            UnitOfWork.Context.Links.AddRange(Links);
            return Links.Count();
        }

        public int BulkInsert(IEnumerable<Link> Links)
        {
            StringBuilder insertQuery = new($"INSERT INTO `Link` (`ParentId`, `ChildId`, `Level`, `RowNumber`) VALUES ");

            foreach (var item in Links)
            {
                insertQuery.Append($"({item.EntityId},{item.RelatedEntityId},{item.Level},{item.RowNumber}");
            }

            insertQuery.Length -= 1;

            var result = UnitOfWork.Context.Database.ExecuteSqlRaw(insertQuery.ToString());
            return result;

        }

        public IEnumerable<Link> FindBy(Expression<System.Func<Link, bool>> predicate)
        {
            return UnitOfWork.Context.Links.Where(predicate);
        }

        public Task<IEnumerable<Link>> FindByAsync(Expression<System.Func<Link, bool>> predicate)
        {
            return new TaskFactory().StartNew(() => FindBy(predicate));
        }

        public Link Get(long id)
        {
            return UnitOfWork.Context.Links.SingleOrDefault(child => child.Id == id);
        }

        public IEnumerable<Link> GetAll()
        {
            return UnitOfWork.Context.Links.AsEnumerable();
        }

        public Task<Link> GetAsync(long id)
        {
            return new TaskFactory().StartNew(() => Get(id));
        }

        public Link Add(Link entity)
        {
            return UnitOfWork.Context.Links.Add(entity).Entity;
        }

        public Task<Link> AddAsync(Link entity)
        {
            return new TaskFactory().StartNew(() => Add(entity));
        }

        public void Remove(Link entity)
        {
            UnitOfWork.Context.Links.Remove(entity);
        }

        public Link Update(Link entity)
        {
            UnitOfWork.Context.Links.Update(entity);
            return entity;
        }

        public void RemoveChildLinks(long parentId)
        {
            UnitOfWork.Context.Database.ExecuteSqlRaw($"DELETE FROM INTO `Link` WHERE EntityId = {parentId}");
        }
    }
}
