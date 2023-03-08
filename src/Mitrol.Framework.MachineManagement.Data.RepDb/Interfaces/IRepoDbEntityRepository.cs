namespace Mitrol.Framework.MachineManagement.Data.RepDb.Interfaces
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;

    public interface IRepoDbEntityRepository: IRepository<MasterEntity, IRepoDbDatabaseContext>
    {
        int RawUpdate(MasterEntity entity);
    }
}
