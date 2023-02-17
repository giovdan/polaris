namespace RepoDbVsEF.Application.Interfaces
{
    using RepoDbVsEF.Application.Models;
    using System.Collections.Generic;

    public interface IEntityService
    {
        Entity Create(Entity entity);
        Entity Update(Entity entity);
        void Delete(ulong entityId);
        IEnumerable<Entity> GetAll();
        Entity Get(ulong entityId);
    }
}
