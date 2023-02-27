namespace RepoDbVsEF.GraphQL.Core
{
    using HotChocolate;
    using RepoDbVsEF.Application.Interfaces;
    using RepoDbVsEF.Application.Models;
    using RepoDbVsEF.Domain.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class Query
    {
        public IQueryable<Entity> GetEntities(IEntityService entityService)
        {
            entityService.SetSession(NullUserSession.Instance);
            return entityService.GetAll().AsQueryable();
        }

        public Entity GetEntity(IEntityService entityService, long id)
        {
            entityService.SetSession(NullUserSession.Instance);
            var result = entityService.Get(id);
            return result.Success ? result.Value : null;
        }
    }
}
