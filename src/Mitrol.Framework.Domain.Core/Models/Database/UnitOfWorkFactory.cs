namespace Mitrol.Framework.Domain.Core.Models
{
    using Microsoft.EntityFrameworkCore.Storage;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System.Threading.Tasks;

    public class UnitOfWorkFactory : IUnitOfWorkFactory<IDatabaseContext>
    {
        private readonly IServiceFactory _serviceFactory;

        private IUnitOfWork<IDatabaseContext> CurrentUnitOfWork { get; set; }

        public UnitOfWorkFactory(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
            CurrentUnitOfWork = null;
        }

     
        

        public IUnitOfWork<IDatabaseContext> GetOrCreate(IUserSession userSession)
        {
            if (CurrentUnitOfWork == null)
            {
                CurrentUnitOfWork = _serviceFactory.GetService<IUnitOfWork<IDatabaseContext>>();
                CurrentUnitOfWork.UserSession = userSession ?? NullUserSession.Instance;
            }

            return CurrentUnitOfWork;
        }

        public void Dispose()
        {
            CurrentUnitOfWork.Dispose();
        }
    }
}