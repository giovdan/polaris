namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;

    public interface IEntityRepository : IRepository<Entity, IMachineManagentDatabaseContext>
    {
        Entity RawUpdate(Entity entity);
    }
}
