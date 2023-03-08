namespace Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;

    public interface IEFEntityRepository : IRepository<MasterEntity, IEFDatabaseContext>
    {
        MasterEntity RawUpdate(MasterEntity entity);
    }
}
