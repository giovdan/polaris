namespace Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;

    public interface IEntityLinkRepository: IRepository<EntityLink, IEFDatabaseContext>
    {
        void RemoveChildLinks(long parentId);
    }
}
