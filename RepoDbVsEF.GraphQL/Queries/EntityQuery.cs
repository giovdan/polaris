namespace RepoDbVsEF.GraphQL.Queries
{
    using HotChocolate;
    using RepoDbVsEF.Application.Interfaces;
    using RepoDbVsEF.Application.Models;
    using System.Linq;

    public class EntityQuery
    {
        public EntityQuery()
        {

        }

        public IQueryable<Entity> GetEntities(IEntityService service) => service.GetAll().AsQueryable();
    }
}
