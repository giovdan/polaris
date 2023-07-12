namespace Mitrol.Framework.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Models;
    using System.Collections.Generic;

    public interface IRemoteToolService: IApplicationService
    {
        IEnumerable<ToolItem> GetToolIdentifiers();
        IEnumerable<ToolItem> GetToolIdentifiers(ToolItemIdentifiersFilter filter);

        ToolItem GetTool(ToolItemFilter filter);
    }
}