using RepoDb;
namespace RepoDbVsEF.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using MySql.Data.MySqlClient;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Data.RepDb.Interfaces;

    public class InternalBaseRepository<T>: BaseRepository<T, MySqlConnection>
        where T: BaseEntity
    {

        public InternalBaseRepository(string connectionString):base(connectionString)
        {

        }

        internal IUnitOfWork<IRepoDbDatabaseContext> UnitOfWork { get; set; }

        public virtual void Attach(IUnitOfWork<IRepoDbDatabaseContext> unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public T Add(T entity)
        {
            try
            {
                entity.Id = Insert<long>(entity, transaction: UnitOfWork.CurrentTransaction);
                return entity;
            }
            catch 
            {
                throw;
            }
        }

        public Task<T> AddAsync(T entity)
        {
            return Task.Factory.StartNew(() => Add(entity));
        }

        public int BatchInsert(IEnumerable<T> items)
        {
            return InsertAll(items, transaction: UnitOfWork.CurrentTransaction);
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            return Query(predicate);
        }

        public Task<IEnumerable<T>> FindByAsync(Expression<Func<T, bool>> predicate)
        {
            return Task.Factory.StartNew(() => FindBy(predicate));
        }

        public IEnumerable<T> GetAll()
        {
            return QueryAll();
        }

        public T Get(long id)
        {
            return Query(s => s.Id == id).SingleOrDefault();
        }

        public Task<T> GetAsync(long id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }
        
    }
}
