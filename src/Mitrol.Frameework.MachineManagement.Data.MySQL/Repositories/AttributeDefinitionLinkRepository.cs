namespace Mitrol.Framework.MachineManagement.Data.MySQL.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading.Tasks;

    public class AttributeDefinitionLinkRepository : BaseRepository<AttributeDefinitionLink>
                                                , IAttributeDefinitionLinkRepository
    {
        public AttributeDefinitionLinkRepository(IServiceFactory serviceFactory, IDatabaseContextFactory databaseContextFactory) : base(serviceFactory, databaseContextFactory)
        {

        }

        public IEnumerable<AttributeDefinitionLink> FindBy(Expression<Func<AttributeDefinitionLink, bool>> predicate)
        {
            return UnitOfWork.Context.AttributeDefinitionLinks
                .Include(adl => adl.AttributeDefinition)
                .Where(predicate);
        }

        public IEnumerable<AttributeDefinitionLink> FindBy<TKey>(Expression<Func<AttributeDefinitionLink, bool>> predicate
                                                        , Expression<Func<AttributeDefinitionLink, TKey>> orderBy)
        {
            return UnitOfWork.Context.AttributeDefinitionLinks
                .Include(adl => adl.AttributeDefinition)
                .Where(predicate).OrderBy(orderBy).AsEnumerable();
        }

        public Task<IEnumerable<AttributeDefinitionLink>> FindByAsync(Expression<Func<AttributeDefinitionLink, bool>> predicate)
        {
            return Task.Factory.StartNew(() => FindBy(predicate));
        }

        public AttributeDefinitionLink Get(long id)
        {
            return UnitOfWork.Context.AttributeDefinitionLinks
                    .Include(adl => adl.AttributeDefinition)
                    .SingleOrDefault(adl => adl.Id == id);
        }

        public IEnumerable<AttributeDefinitionLink> GetAll()
        {
            return UnitOfWork.Context.AttributeDefinitionLinks
                .Include(adl => adl.AttributeDefinition);
        }

        public Task<AttributeDefinitionLink> GetAsync(long id)
        {
            return Task.Factory.StartNew(() => Get(id));
        }
    }
}
