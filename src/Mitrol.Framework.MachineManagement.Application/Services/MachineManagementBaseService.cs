


namespace Mitrol.Framework.MachineManagement.Application.Services
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;

    public class MachineManagementBaseService: BaseService
    {
        protected IUnitOfWorkFactory<IMachineManagentDatabaseContext> UnitOfWorkFactory => ServiceFactory
                    .GetService<IUnitOfWorkFactory<IMachineManagentDatabaseContext>>();

        public MachineManagementBaseService(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }
    }
}
