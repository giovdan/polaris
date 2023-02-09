namespace RepoDbVsEF.Domain.Interfaces
{
    public interface IDatabaseContextFactory
    {
        IDatabaseContext Create();
    }

    public static class IDatabaseContextFactoryExtensions
    {
        public static TDbContext Create<TDbContext>(this IDatabaseContextFactory databaseContextFactory)
            where TDbContext : class, IDatabaseContext
        {
            return databaseContextFactory.Create() as TDbContext;
        }
    }
}
