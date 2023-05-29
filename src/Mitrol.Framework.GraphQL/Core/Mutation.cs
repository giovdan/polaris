

namespace Mitrol.Framework.GraphQL.Core
{
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;

    public class Mutation
    {
        public ToolDetailItem CreateTool(IToolService toolService, ToolDetailItem tool)
        {
            toolService.SetSession(NullUserSession.InternalSessionInstance);
            var result = toolService.CreateTool(tool);
            return result.Success ? result.Value : null;
        }
    }
}
