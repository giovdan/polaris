namespace Mitrol.Framework.Domain.Macro
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    public interface IToolManagementFilter : IDataExternalInterface
    {
        ToolTypeEnum ToolType { get; }
    }

    public interface IToolManagement
    {
        Result<ExternalBaseData> GetToolAttributes(IToolManagementFilter filter);
    }
}
