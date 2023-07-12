
namespace Mitrol.Framework.MachineManagement.Data.MySQL.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class DetailIdentifierRepository : BaseRepository<DetailIdentifier>
                                    , IDetailIdentifierRepository
    {
        public DetailIdentifierRepository(IServiceFactory serviceFactory
                , IDatabaseContextFactory databaseContextFactory) : base(serviceFactory, databaseContextFactory)
        {

        }

        

        public IEnumerable<DetailIdentifier> FindBy(Expression<Func<DetailIdentifier, bool>> predicate) 
                => UnitOfWork.Context.DetailIdentifiers.Include(di => di.AttributeDefinitionLink).Where(predicate);

        public IEnumerable<DetailIdentifier> FindBy<TKey>(Expression<Func<DetailIdentifier, bool>> predicate
            , Expression<Func<DetailIdentifier, TKey>> orderBy) => UnitOfWork.Context.DetailIdentifiers
                    .Include(di => di.AttributeDefinitionLink)
                    .ThenInclude(di => di.AttributeDefinition)
                    .Where(predicate)
                    .OrderBy(orderBy);

        public Task<IEnumerable<DetailIdentifier>> FindByAsync(Expression<Func<DetailIdentifier, bool>> predicate) 
            => Task.Factory.StartNew(() => FindBy(predicate));

        public DetailIdentifier Get(long id) => UnitOfWork.Context.DetailIdentifiers
                    .Include(di => di.AttributeDefinitionLink)
                    .ThenInclude(di => di.AttributeDefinition).SingleOrDefault(di => di.Id == id);

        public Task<DetailIdentifier> GetAsync(long id) => Task.Factory.StartNew(() => Get(id));

        public IEnumerable<DetailIdentifier> GetAll() => UnitOfWork.Context
                                .DetailIdentifiers
            .Include(di => di.AttributeDefinitionLink)
            .ThenInclude(link => link.AttributeDefinition);

        public IEnumerable<DetailIdentifierMaster> GetIdentifiers(Expression<Func<DetailIdentifierMaster, bool>> predicate)
                    => UnitOfWork.Context.DetailIdentifierMasters.Where(predicate);
    }
}
