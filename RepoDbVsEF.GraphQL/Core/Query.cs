namespace RepoDbVsEF.GraphQL.Core
{
    using HotChocolate;
    using RepoDbVsEF.Application.Interfaces;
    using RepoDbVsEF.Application.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class Query
    {
        private IEntityService _entityService;
        public Query(IEntityService entityService)
        {
            _entityService = entityService;
        }

        public IQueryable<Entity> GetEntities() => _entityService.GetAll().AsQueryable();
     
    }
}
