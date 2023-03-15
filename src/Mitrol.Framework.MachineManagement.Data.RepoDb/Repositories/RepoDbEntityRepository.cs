using RepoDb;
namespace Mitrol.Framework.MachineManagement.Data.RepDb.Repositories
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Data.RepDb.Interfaces;
    using RepoDbVsEF.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class RepoDbEntityRepository: InternalBaseRepository<MasterEntity>, IRepoDbEntityRepository
    {
        public RepoDbEntityRepository(string connectionString): base(connectionString)
        {

        }

        public MasterEntity Add(MasterEntity entity)
        {
            entity.Id = Insert<long>(entity, transaction: UnitOfWork.CurrentTransaction);
            return entity;
        }

        public Task<MasterEntity> AddAsync(MasterEntity entity)
        {
            return Task.Factory.StartNew(() => Add(entity));
        }

        public void Attach(IUnitOfWork<IRepoDbDatabaseContext> unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public int BatchInsert(IEnumerable<MasterEntity> items)
        {
            return InsertAll(items);
        }

        public int BulkInsert(IEnumerable<MasterEntity> items)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MasterEntity> FindBy(Expression<Func<MasterEntity, bool>> predicate)
        {
            return Query(predicate);
        }

        public Task<IEnumerable<MasterEntity>> FindByAsync(Expression<Func<MasterEntity, bool>> predicate)
        {
            return Task.Factory.StartNew(() => FindBy(predicate));
        }

        public MasterEntity Get(long id)
        {
            return Query(e => e.Id == id).SingleOrDefault();
        }

        public IEnumerable<MasterEntity> GetAll()
        {
            return QueryAll();
        }

        public Task<MasterEntity> GetAsync(long id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }

        public int RawUpdate(MasterEntity entity)
        {
            return UnitOfWork.Context.Connection.ExecuteNonQuery($"UPDATE entity SET DisplayName = '{entity.DisplayName}' WHERE Id = {entity.Id}");
        }

        public void Remove(MasterEntity entity)
        {
            Delete(entity, transaction: UnitOfWork.CurrentTransaction);
        }

        public MasterEntity Update(MasterEntity entity)
        {
            Update(entity, transaction: UnitOfWork.CurrentTransaction);
            return entity;
        }
    }
}
