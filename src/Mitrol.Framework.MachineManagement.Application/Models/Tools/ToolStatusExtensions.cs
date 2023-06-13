
namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public static class ToolStatusExtensions
    {
        public static EntityStatusEnum CheckIfToolIsUnitEnabled(this IExecutionService service,
                                                          IEnumerable<AttributeDetailItem> toolStatusAttributes,
                                                          PlantUnitEnum plantUnit)
        {
            var toolStatus = EntityStatusEnum.Available;


            var enabledToolFlags = service.GetUnitSetupList()
                    .Where(su => su.UnitType == plantUnit)
                    .Select(su => su.UnitId.GetRelatedAttribute());

            Expression<Func<AttributeDetailItem, bool>> predicate = a => false;

            foreach (var enabledToolFlag in enabledToolFlags)
            {
                predicate = predicate.OrElse(a => a.EnumId == enabledToolFlag);
            }

            var unitAttributes = toolStatusAttributes.Where(predicate.Compile()).ToList();

            if (unitAttributes.Any() && unitAttributes.All(unit => Convert.ToBoolean(unit.Value.CurrentValue) == false))
                toolStatus = EntityStatusEnum.Unavailable;

            return toolStatus;
        }
    }
}
