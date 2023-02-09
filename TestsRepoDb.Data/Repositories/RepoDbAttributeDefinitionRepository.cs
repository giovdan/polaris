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
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AttributeDefinition>> FindByAsync(Expression<Func<AttributeDefinition, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public AttributeDefinition Get(ulong id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AttributeDefinition> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AttributeDefinition> GetAsync(ulong id)
        {
            throw new NotImplementedException();
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
