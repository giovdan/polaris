namespace Mitrol.Framework.MachineManagement.Data.MySQL.Repositories
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class AttributeDefinitionRepository: BaseRepository<AttributeDefinition>
                , IAttributeDefinitionRepository
    {
        public AttributeDefinitionRepository(IServiceFactory serviceFactory
                        , IDatabaseContextFactory databaseContextFactory) : base(serviceFactory, databaseContextFactory)
        {

        }

        public IEnumerable<AttributeDefinition> FindBy(Expression<Func<AttributeDefinition, bool>> predicate)
        {
            return UnitOfWork.Context.AttributeDefinitions.Where(predicate);
        }

        public IEnumerable<AttributeDefinition> FindBy<TKey>
            (Expression<Func<AttributeDefinition, bool>> predicate
                , Expression<Func<AttributeDefinition, TKey>> orderBy)
        {
            return UnitOfWork.Context.AttributeDefinitions.Where(predicate).OrderBy(orderBy);
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
        

    }
}
