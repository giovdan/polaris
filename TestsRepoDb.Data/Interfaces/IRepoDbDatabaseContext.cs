namespace RepoDbVsEF.Data.Interfaces
{
    using System.Data;
    using RepoDbVsEF.Domain.Interfaces;

    public interface IRepoDbDatabaseContext: IDatabaseContext
    {
        IDbConnection Connection { get; }
    }
}
