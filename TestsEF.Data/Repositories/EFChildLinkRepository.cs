﻿namespace Mitrol.Framework.MachineManagement.Data.MySQL.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public class EFChildLinkRepository : BaseRepository<ChildLink>, IChildLinkRepository
    {
        protected IServiceFactory ServiceFactory { get; private set; }

        public EFChildLinkRepository(IServiceFactory serviceFactory, IDatabaseContextFactory databaseContextFactory) : base(serviceFactory, databaseContextFactory)
        {

        }

        public int BatchInsert(IEnumerable<ChildLink> childLinks)
        {
            UnitOfWork.Context.ChildLinks.AddRange(childLinks);
            return childLinks.Count();
        }

        public int BulkInsert(IEnumerable<ChildLink> childLinks)
        {
            StringBuilder insertQuery = new($"INSERT INTO `ChildLink` (`ParentId`, `ChildId`, `Level`, `RowNumber`) VALUES ");

            foreach (var item in childLinks)
            {
                insertQuery.Append($"({item.ParentId},{item.ChildId},{item.Level},{item.RowNumber}");
            }

            insertQuery.Length -= 1;

            var result = UnitOfWork.Context.Database.ExecuteSqlRaw(insertQuery.ToString());
            return result;

        }

        public IEnumerable<ChildLink> FindBy(Expression<System.Func<ChildLink, bool>> predicate)
        {
            return UnitOfWork.Context.ChildLinks.Where(predicate);
        }

        public Task<IEnumerable<ChildLink>> FindByAsync(Expression<System.Func<ChildLink, bool>> predicate)
        {
            return new TaskFactory().StartNew(() => FindBy(predicate));
        }

        public ChildLink Get(long id)
        {
            return UnitOfWork.Context.ChildLinks.SingleOrDefault(child => child.Id == id);
        }

        public IEnumerable<ChildLink> GetAll()
        {
            return UnitOfWork.Context.ChildLinks.AsEnumerable();
        }

        public Task<ChildLink> GetAsync(long id)
        {
            return new TaskFactory().StartNew(() => Get(id));
        }

        public ChildLink Add(ChildLink entity)
        {
            return UnitOfWork.Context.ChildLinks.Add(entity).Entity;
        }

        public Task<ChildLink> AddAsync(ChildLink entity)
        {
            return new TaskFactory().StartNew(() => Add(entity));
        }

        public void Remove(ChildLink entity)
        {
            UnitOfWork.Context.ChildLinks.Remove(entity);
        }

        public ChildLink Update(ChildLink entity)
        {
            UnitOfWork.Context.ChildLinks.Update(entity);
            return entity;
        }

        public void RemoveLinks(long parentId)
        {
            UnitOfWork.Context.Database.ExecuteSqlRaw($"DELETE FROM INTO `ChildLink` WHERE ParentId = {parentId}");
        }
    }
}
