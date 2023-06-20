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
    using System.Linq;

    public sealed class ExecutionService : BaseServiceWithEvents, IBootableService, IExecutionService
    {
        public ExecutionService(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }

        public Result Boot(IUserSession userSession)
        {
            return Result.Ok();
        }

        public Result CleanUpBeforeBoot(IUserSession userSession)
        {
            return Result.Ok();
        }

        public IEnumerable<UnitSetupListItem> GetUnitSetupList()
        {
            return Enumerable.Empty<UnitSetupListItem>();
        }

        public Result StartExecution(IUserSession userSession)
        {
            return Result.Ok();
        }

        public Result StopExecution(IUserSession userSession)
        {
            return Result.Ok();
        }

     
    }
}
