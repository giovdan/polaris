namespace Mitrol.Framework.MachineManagement.Application.Services
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models.Setup;
    using System;
    using System.Collections.Generic;

    public sealed class ExecutionService : BaseServiceWithEvents, IBootableService, IExecutionService
    {
        public ExecutionService(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }

        public Result Boot(IUserSession userSession)
        {
            throw new NotImplementedException();
        }

        public Result CleanUpBeforeBoot(IUserSession userSession)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UnitSetupListItem> GetUnitSetupList()
        {
            throw new NotImplementedException();
        }

        public Result StartExecution(IUserSession userSession)
        {
            throw new NotImplementedException();
        }

        public Result StopExecution(IUserSession userSession)
        {
            throw new NotImplementedException();
        }

     
    }
}
