namespace Mitrol.Framework.Domain.Core.Models
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Models;

    public class BaseRepository<TDbContext> : Disposable
        where TDbContext : BaseDbContext
    {
        public BaseRepository(IDatabaseContextFactory databaseContextFactory)
        {
            DatabaseContextFactory = databaseContextFactory;
        }

        protected override void DisposeManaged()
        {
            UnitOfWork?.Dispose();
            base.DisposeManaged();
        }

        public IUnitOfWork<TDbContext> UnitOfWork { get; set; }
        protected IDatabaseContextFactory DatabaseContextFactory { get; }

        /// <summary>
        /// Default implementation of unit of work
        /// </summary>
        /// <param name="unitOfWork"></param>
        public void SetUnitOfWork(IUnitOfWork<TDbContext> unitOfWork)
        {
            if (UnitOfWork == null ||
                (UnitOfWork != null && unitOfWork.Id != UnitOfWork.Id))
            {
                UnitOfWork?.Dispose();
                UnitOfWork = unitOfWork;
            }
        }

		public TDbContext Context
        {
            get
            {
                if (UnitOfWork?.IsFinished ?? true)
                {
                    return DatabaseContextFactory.Create<TDbContext>();
                }
                else
                {
                    return UnitOfWork.Context as TDbContext;
                }
            }
        }
    }
}