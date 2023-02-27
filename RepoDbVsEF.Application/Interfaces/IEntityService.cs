namespace RepoDbVsEF.Application.Interfaces
{
    using RepoDbVsEF.Application.Core;
    using RepoDbVsEF.Application.Models;
    using RepoDbVsEF.Domain.Models;
    using System.Collections.Generic;

    public interface IEntityService: IApplicationService
    {
        Result<Entity> Create(Entity entity);
        Result<EntityWithChildren> CreateEntityWithChildren(EntityWithChildren entity);
        Result<Entity> Update(Entity entity);
        Result Delete(long entityId);
        IEnumerable<Entity> GetAll();
        Result<Entity> Get(long entityId);
        Result<long[]> BulkCreate(IEnumerable<Entity> entities);
        Result<long[]> BatchCreate(IEnumerable<Entity> entities);
    }
}
