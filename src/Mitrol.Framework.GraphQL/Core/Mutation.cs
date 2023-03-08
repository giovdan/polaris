

namespace Mitrol.Framework.GraphQL.Core
{
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;

    public class Mutation
    {
        public EntityItem CreateEntity(IEntityService entityService, EntityItem entity)
        {
            entityService.SetSession(NullUserSession.Instance);
            var result = entityService.Create(entity);
            return result.Success? result.Value : null;
        }
    }
}
