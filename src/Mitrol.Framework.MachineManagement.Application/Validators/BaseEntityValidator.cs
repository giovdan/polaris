
namespace Mitrol.Framework.MachineManagement.Application.Validators
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using System.Collections.Generic;

    public class BaseEntityValidator<IDatabaseConnection>
    {
        internal Dictionary<DatabaseDisplayNameEnum, object> AdditionalInfos { get; set; }
        internal IServiceFactory ServiceFactory { get; set; }
        internal IUnitOfWork<IDatabaseConnection> UnitOfWork { get; set; }

        public BaseEntityValidator(IServiceFactory serviceFactory)
        {
            Init(serviceFactory, new Dictionary<DatabaseDisplayNameEnum, object>());
        }

        public void Init(IServiceFactory serviceFactory, Dictionary<DatabaseDisplayNameEnum, object> additionalInfos)
        {
            ServiceFactory = serviceFactory;
            AdditionalInfos = additionalInfos;
        }

        public void Init(Dictionary<DatabaseDisplayNameEnum, object> additionalInfos)
        {
            AdditionalInfos = additionalInfos;
        }

        public void Attach(IUnitOfWork<IDatabaseConnection> unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
    }
}
