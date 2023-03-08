namespace Mitrol.Framework.MachineManagement.Data.RepDb.Interfaces
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;

    public interface IChildLinkRepository : IRepository<ChildLink, IRepoDbDatabaseContext>
    {
        void RemoveLinks(long parentId);
    }
}
