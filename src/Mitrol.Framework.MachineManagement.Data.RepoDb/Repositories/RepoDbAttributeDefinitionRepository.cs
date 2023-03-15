namespace Mitrol.Framework.MachineManagement.Data.RepDb.Repositories
{
    using global::RepoDb;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Data.RepDb.Interfaces;
    using RepoDbVsEF.Data.Repositories;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class RepoDbAttributeDefinitionRepository : InternalBaseRepository<AttributeDefinition>
                        , IRepoDbAttributeDefinitionRepository
    {
        public RepoDbAttributeDefinitionRepository(string connectionString) : base(connectionString)
        {

        }

        public AttributeDefinition Add(AttributeDefinition entity)
        {
            throw new NotImplementedException();
        }

        public Task<AttributeDefinition> AddAsync(AttributeDefinition entity)
        {
            throw new NotImplementedException();
        }

        public void Attach(IUnitOfWork<IRepoDbDatabaseContext> unitOfWork)
        {
            throw new NotImplementedException();
        }

        public int BatchInsert(IEnumerable<AttributeDefinition> items)
        {
            throw new NotImplementedException();
        }

        public int BulkInsert(IEnumerable<AttributeDefinition> items)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeDefinition> FindBy(Expression<Func<AttributeDefinition, bool>> predicate)
        {
            return Query(predicate);
        }

        public Task<IEnumerable<AttributeDefinition>> FindByAsync(Expression<Func<AttributeDefinition, bool>> predicate)
        {
            return Task.Factory.StartNew(() => FindBy(predicate));
        }

        public AttributeDefinition Get(long id)
        {
            return Get(id);
        }

        public IEnumerable<AttributeDefinition> GetAll()
        {
            return GetAll();
        }

        public Task<AttributeDefinition> GetAsync(long id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }

        public void Remove(AttributeDefinition entity)
        {
            throw new NotImplementedException();
        }

        public AttributeDefinition Update(AttributeDefinition entity)
        {
            var result = UnitOfWork.Context.Connection.Update(entity);

            return result > 0 ? Get(entity.Id): null;
        }

        public bool UpdateBulk(AttributeDefinition entity)
        {
            string query = $"UPDATE entity SET DisplayName = '{entity.DisplayName}', RowVersion = RowVersion+1 WHERE Id = {entity.Id}";

            return UnitOfWork.Context.Connection.ExecuteNonQuery(query) > 0;
        }
    }
}
