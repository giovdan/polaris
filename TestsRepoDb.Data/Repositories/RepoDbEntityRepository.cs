namespace RepoDbVsEF.RepoDb.Data.Repositories
{
    using RepoDbVsEF.Data.Interfaces;
    using RepoDbVsEF.Data.Repositories;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.Domain.Models;
    using RepoDbVsEF.RepoDb.Data.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class RepoDbEntityRepository: InternalBaseRepository<Entity>, IRepoDbEntityRepository
    {
        public RepoDbEntityRepository(string connectionString): base(connectionString)
        {

        }

        public Entity Add(Entity entity)
        {
            entity.Id = Insert<ulong>(entity, transaction: UnitOfWork.CurrentTransaction);
            return entity;
        }

        public Task<Entity> AddAsync(Entity entity)
        {
            return Task.Factory.StartNew(() => Add(entity));
        }

        public void Attach(IUnitOfWork<IRepoDbDatabaseContext> unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public int BatchInsert(IEnumerable<Entity> items)
        {
            return InsertAll(items);
        }

        public int BulkInsert(IEnumerable<Entity> items)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entity> FindBy(Expression<Func<Entity, bool>> predicate)
        {
            return Query(predicate);
        }

        public Task<IEnumerable<Entity>> FindByAsync(Expression<Func<Entity, bool>> predicate)
        {
            return Task.Factory.StartNew(() => FindBy(predicate));
        }

        public Entity Get(ulong id)
        {
            return Query(e => e.Id == id).SingleOrDefault();
        }

        public IEnumerable<Entity> GetAll()
        {
            return QueryAll();
        }

        public Task<Entity> GetAsync(ulong id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }

        public long RawUpdate(Entity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Entity entity)
        {
            Delete(entity, transaction: UnitOfWork.CurrentTransaction);
        }

        public Entity Update(Entity entity)
        {
            Update(entity, transaction: UnitOfWork.CurrentTransaction);
            return entity;
        }
    }
}
