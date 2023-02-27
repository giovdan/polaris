

namespace RepoDbVsEF.GraphQL.Core
{
    using RepoDbVsEF.Application.Interfaces;
    using RepoDbVsEF.Application.Models;
    using RepoDbVsEF.Domain.Models;

    public class Mutation
    {
        public Entity CreateEntity(IEntityService entityService, Entity entity)
        {
            entityService.SetSession(NullUserSession.Instance);
            var result = entityService.Create(entity);
            return result.Success? result.Value : null;
        }
    }
}
