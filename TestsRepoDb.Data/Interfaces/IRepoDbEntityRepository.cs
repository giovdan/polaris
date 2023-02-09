namespace RepoDbVsEF.RepoDb.Data.Interfaces
{
    using RepoDbVsEF.Data.Interfaces;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.Domain.Models;

    public interface IRepoDbEntityRepository: IRepository<Entity, IRepoDbDatabaseContext>
    {
        long RawUpdate(Entity entity);
    }
}
