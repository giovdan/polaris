namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System.Collections.Generic;

    public interface IMachineManagementService: IApplicationService
    {
        IEnumerable<AttributeDetailItem> GetAttributeDefinitions(EntityTypeEnum entityType);
    }
}
