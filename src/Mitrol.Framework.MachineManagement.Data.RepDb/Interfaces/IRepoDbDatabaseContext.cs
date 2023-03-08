namespace Mitrol.Framework.MachineManagement.Data.RepDb.Interfaces
{
    using System.Data;
    using Mitrol.Framework.Domain.Interfaces;

    public interface IRepoDbDatabaseContext: IDatabaseContext
    {
        IDbConnection Connection { get; }
    }
}
