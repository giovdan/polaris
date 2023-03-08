namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Core;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using System.Collections.Generic;

    public interface IEntityService: IApplicationService
    {
        Result<EntityItem> Create(EntityItem entity);
        Result<EntityWithChildren> CreateEntityWithChildren(EntityWithChildren entity);
        Result<EntityItem> Update(EntityItem entity);
        Result Delete(long entityId);
        IEnumerable<EntityListItem> GetAll();
        Result<EntityItem> Get(long entityId);
        Result<long[]> BulkCreate(IEnumerable<EntityItem> entities);
        Result<long[]> BatchCreate(IEnumerable<EntityItem> entities);
        IEnumerable<AttributeItem> GetAttributesByType(EntityTypeEnum type);
    }
}
