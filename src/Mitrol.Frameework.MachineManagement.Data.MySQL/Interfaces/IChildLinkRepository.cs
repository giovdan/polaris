namespace Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    

    public interface IChildLinkRepository: IRepository<ChildLink, IEFDatabaseContext>
    {
        void RemoveLinks(long parentId);
    }
}
