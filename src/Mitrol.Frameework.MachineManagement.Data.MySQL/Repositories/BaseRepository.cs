namespace Mitrol.Framework.MachineManagement.Data.MySQL.Repositories
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces;

    public class BaseRepository<TEntity>
        where TEntity: BaseEntity
    {
        public IUnitOfWork<IEFDatabaseContext> UnitOfWork { get; set; }
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
