namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;

    public interface IEntityRepository : IRepository<Entity, IEFDatabaseContext>
    {
        Entity RawUpdate(Entity entity);
    }
}
