namespace RepoDbVsEF.EF.Data.Repositories
{
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.Domain.Models;
    using RepoDbVsEF.EF.Data.Interfaces;

    public class BaseRepository<TEntity>
        where TEntity: BaseEntity
    {
        internal IUnitOfWork<IEFDatabaseContext> UnitOfWork { get; set; }
        protected IDatabaseContextFactory DatabaseContextFactory { get; }

        public virtual void Attach(IUnitOfWork<IEFDatabaseContext> unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        public BaseRepository(IServiceFactory serviceFactory, IDatabaseContextFactory databaseContextFactory)
        {
            DatabaseContextFactory = databaseContextFactory;
        }


        
    }
}
