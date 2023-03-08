namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Core;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using System.Collections.Generic;

    public interface IEntityService: IApplicationService
    {
        Result<Entity> Create(Entity entity);
        Result<EntityWithChildren> CreateEntityWithChildren(EntityWithChildren entity);
        Result<Entity> Update(Entity entity);
        Result Delete(long entityId);
        IEnumerable<EntityListItem> GetAll();
        Result<Entity> Get(long entityId);
        Result<long[]> BulkCreate(IEnumerable<Entity> entities);
        Result<long[]> BatchCreate(IEnumerable<Entity> entities);
        IEnumerable<AttributeItem> GetAttributesByType(EntityTypeEnum type);
    }
}
