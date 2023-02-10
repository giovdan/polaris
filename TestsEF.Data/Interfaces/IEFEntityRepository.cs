namespace RepoDbVsEF.EF.Data.Interfaces
{
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.Domain.Models;

    public interface IEFEntityRepository : IRepository<Entity, IEFDatabaseContext>
    {
        Entity RawUpdate(Entity entity);
    }
}
