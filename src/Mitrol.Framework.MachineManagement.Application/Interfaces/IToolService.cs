﻿
namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using System.Collections.Generic;

    public interface IToolService: IMachineManagementService, IBootableService
    {
        Result<ToolDetailItem> CreateTool(ToolDetailItem toolDetail);
        IEnumerable<ToolListItem> GetAll();
        Result<ToolDetailItem> Get(long toolId
                    , bool onlyQuickAccess = false
                    , MeasurementSystemEnum conversionSystemFrom = MeasurementSystemEnum.MetricSystem
                    , MeasurementSystemEnum conversionSystemTo = MeasurementSystemEnum.MetricSystem);
        Result<ToolDetailItem> GetByToolManagementId(int toolMnagementId
                    , bool onlyQuickAccess = false
                    , MeasurementSystemEnum conversionSystemFrom = MeasurementSystemEnum.MetricSystem
                    , MeasurementSystemEnum conversionSystemTo = MeasurementSystemEnum.MetricSystem);
        ToolDetailItem GetToolTemplateForCreation(AttributeDefinitionFilter filters);
        Result Remove(long toolId);
        Result<ToolDetailItem> UpdateTool(ToolDetailItem tool);
    }
}
