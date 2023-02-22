namespace RepoDbVsEF.GraphQL.Core
{
    using HotChocolate;
    using RepoDbVsEF.Application.Interfaces;
    using RepoDbVsEF.Application.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class Query
    {
        public IQueryable<Entity> GetEntities(IEntityService entityService) => entityService.GetAll().AsQueryable();

        
    }
}
