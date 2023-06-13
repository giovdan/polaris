namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;

    public interface IEntityLinkRepository: IRepository<EntityLink, IMachineManagentDatabaseContext>
    {
        void RemoveChildLinks(long parentId);
    }
}
