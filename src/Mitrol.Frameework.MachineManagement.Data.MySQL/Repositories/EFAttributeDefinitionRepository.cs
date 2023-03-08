namespace Mitrol.Framework.MachineManagement.Data.MySQL.Repositories
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class EFAttributeDefinitionRepository: BaseRepository<AttributeDefinition>, IEFAttributeDefinitionRepository
    {
        public EFAttributeDefinitionRepository(IServiceFactory serviceFactory
                        , IDatabaseContextFactory databaseContextFactory) : base(serviceFactory, databaseContextFactory)
        {

        }

        #region < Read >
        public IEnumerable<AttributeDefinition> FindBy(Expression<Func<AttributeDefinition, bool>> predicate)
        {
            return UnitOfWork.Context.AttributeDefinitions.Where(predicate);
        }

        public Task<IEnumerable<AttributeDefinition>> FindByAsync(Expression<Func<AttributeDefinition, bool>> predicate)
        {
            return Task.Factory.StartNew(() => FindBy(predicate));
        }

        public AttributeDefinition Get(long id)
        {
            return UnitOfWork.Context.AttributeDefinitions.SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<AttributeDefinition> GetAll()
        {
            return UnitOfWork.Context.AttributeDefinitions;
        }

        public Task<AttributeDefinition> GetAsync(long id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }
        #endregion

        public AttributeDefinition Add(AttributeDefinition entity)
        {
            return UnitOfWork.Context.AttributeDefinitions.Add(entity).Entity;
        }

        public Task<AttributeDefinition> AddAsync(AttributeDefinition entity)
        {
            return Task.Factory.StartNew(() => Add(entity));
        }

        public int BatchInsert(IEnumerable<AttributeDefinition> items)
        {
            throw new NotImplementedException();
        }

        public int BulkInsert(IEnumerable<AttributeDefinition> items)
        {
            throw new NotImplementedException();
        }

        

        public void Remove(AttributeDefinition entity)
        {
            throw new NotImplementedException();
        }

        public AttributeDefinition Update(AttributeDefinition entity)
        {
            throw new NotImplementedException();
        }
    }
}
