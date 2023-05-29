namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using System.Collections.Generic;

    public interface IToolAttributesStatus
    {
        (EntityStatusEnum, string) GetToolStatus(IEnumerable<AttributeDetailItem> toolStatusAttributes);
        IEnumerable<AttributeDefinitionEnum> GetToolStatusAttributeDefinitions();
        IEnumerable<AttributeDetailItem> GetAttributesInError(IEnumerable<AttributeDetailItem> toolStatusAttributes);
    }

    public interface IToolBatteryStatus
    {
        (int? Percentage, StatusColorEnum StatusColor) GetBatteryStatus(IEnumerable<ToolStatusAttribute> toolStatusAttributes);
    }

    public interface IToolStatus : IToolBatteryStatus, IToolAttributesStatus
    {
        IServiceFactory ServiceFactory { get; set; }
    }
}
