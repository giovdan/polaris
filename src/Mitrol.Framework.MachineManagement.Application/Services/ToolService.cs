namespace Mitrol.Framework.MachineManagement.Application.Services
{
    using Mitrol.Framework.Domain.Core.Models.Microservices;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using System.Collections.Generic;

    public class ToolService : BaseService, IToolService
    {
        public ToolService(IServiceFactory serviceFactory) : base(serviceFactory)
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

        public Result<ToolDetailItem> CreateTool(ToolDetailItem toolDetail)
        {
            return Result.Ok(new ToolDetailItem());
        }

        public Result<ToolDetailItem> Get(long toolId)
        {
            return Result.Ok(new ToolDetailItem());
        }

        public IEnumerable<ToolListItem> GetAll()
        {
            var entityTypes = ToolDetailItemExtensions.GetToolEntityTypes();
            return null;
        }


    }
}
