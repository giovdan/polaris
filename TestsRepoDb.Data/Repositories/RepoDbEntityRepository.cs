namespace RepoDbVsEF.RepoDb.Data.Repositories
{
    using global::RepoDb;
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

    public class RepoDbEntityRepository: InternalBaseRepository<DatabaseEntity>, IRepoDbEntityRepository
    {
        public RepoDbEntityRepository(string connectionString): base(connectionString)
        {

        }

        public DatabaseEntity Add(DatabaseEntity entity)
        {
            entity.Id = Insert<long>(entity, transaction: UnitOfWork.CurrentTransaction);
            return entity;
        }

        public Task<DatabaseEntity> AddAsync(DatabaseEntity entity)
        {
            return Task.Factory.StartNew(() => Add(entity));
        }

        public void Attach(IUnitOfWork<IRepoDbDatabaseContext> unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public int BatchInsert(IEnumerable<DatabaseEntity> items)
        {
            return InsertAll(items);
        }

        public int BulkInsert(IEnumerable<DatabaseEntity> items)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DatabaseEntity> FindBy(Expression<Func<DatabaseEntity, bool>> predicate)
        {
            return Query(predicate);
        }

        public Task<IEnumerable<DatabaseEntity>> FindByAsync(Expression<Func<DatabaseEntity, bool>> predicate)
        {
            return Task.Factory.StartNew(() => FindBy(predicate));
        }

        public DatabaseEntity Get(long id)
        {
            return Query(e => e.Id == id).SingleOrDefault();
        }

        public IEnumerable<DatabaseEntity> GetAll()
        {
            return QueryAll();
        }

        public Task<DatabaseEntity> GetAsync(long id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }

        public int RawUpdate(DatabaseEntity entity)
        {
            return UnitOfWork.Context.Connection.ExecuteNonQuery($"UPDATE entity SET DisplayName = '{entity.DisplayName}' WHERE Id = {entity.Id}");
        }

        public void Remove(DatabaseEntity entity)
        {
            Delete(entity, transaction: UnitOfWork.CurrentTransaction);
        }

        public DatabaseEntity Update(DatabaseEntity entity)
        {
            Update(entity, transaction: UnitOfWork.CurrentTransaction);
            return entity;
        }
    }
}
