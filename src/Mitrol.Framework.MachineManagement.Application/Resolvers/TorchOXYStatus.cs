namespace Mitrol.Framework.MachineManagement.Application.Resolvers
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TorchOxyStatus : IToolStatus
    {
        private IEntityRepository EntityRepository => ServiceFactory.GetService<IEntityRepository>();
        private IExecutionService ExecutionService => ServiceFactory.GetService<IExecutionService>();

        public IServiceFactory ServiceFactory { get; set; }

        public TorchOxyStatus(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
        }

        public (int? Percentage, StatusColorEnum StatusColor) GetBatteryStatus(IEnumerable<EntityStatusAttribute> toolStatusAttributes)
        {
            var toolLifeMax = toolStatusAttributes.SingleOrDefault
                (a => a.EnumId == AttributeDefinitionEnum.MaxNozzleLifeTime);

            var toolLife = toolStatusAttributes.SingleOrDefault
                (a => a.EnumId == AttributeDefinitionEnum.NozzleLifeTime);

            var warningLife = toolStatusAttributes.SingleOrDefault
                (a => a.EnumId == AttributeDefinitionEnum.NozzleLifeAttentionThreshold);

            // Recupero gli attributi legati alla massima vita ed alla vita corrente
            if (toolLifeMax == null || toolLife == null)
                return (100, StatusColorEnum.Grey);

            // se warningLife è definito ma è 0 allora passo toolLifeMax 
            // se warningLife non è definito allora passo toolLifeMax
            // warningLife è definito ed è 0 allora passo warningLife
            return CoreExtensions.CalculatePercentage(life: toolLife.Value
                                                    , warningThreshold: (warningLife != null && warningLife.Value != 0) ? warningLife.Value : toolLifeMax.Value
                                                    , maxLife: toolLifeMax.Value);
        }

        public (EntityStatusEnum, string) GetToolStatus(IEnumerable<AttributeDetailItem> toolStatusAttributes)
        {
            (var toolStatus, _) = GetToolStatusWithDetail(toolStatusAttributes);
            var toolStatusLocalization = $"{MachineManagementExtensions.LABEL_TOOLSTATUS}_{toolStatus.ToString().ToUpper()}";
            return (toolStatus, toolStatusLocalization);
        }

        private (EntityStatusEnum, IEnumerable<AttributeDetailItem>) GetToolStatusWithDetail(IEnumerable<AttributeDetailItem> toolStatusAttributes)
        {
            var details = new List<AttributeDetailItem>();

            var toolStatus = ExecutionService.CheckIfToolIsUnitEnabled(toolStatusAttributes, PlantUnitEnum.OxyCutTorch);

            var toolLifeMaxAttribute = toolStatusAttributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.MaxNozzleLifeTime);
            var toolLifeMaxValue = Convert.ToDecimal(toolLifeMaxAttribute.Value.CurrentValue);

            var toolLifeAttribute = toolStatusAttributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.NozzleLifeTime);
            var toolLifeValue = Convert.ToDecimal(toolLifeAttribute.Value.CurrentValue);

            var warningLifeAttribute = toolStatusAttributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.NozzleLifeAttentionThreshold);
            var warningLifeValue = Convert.ToDecimal(warningLifeAttribute.Value.CurrentValue);


            toolLifeAttribute.AttributeStatus = AttributeStatusExtensions.SetAttributeStatus(toolLifeMaxValue, toolLifeValue, warningLifeValue);
            if (toolLifeAttribute.AttributeStatus.Status != EntityStatusEnum.Available)
            {
                toolStatus = toolLifeAttribute.AttributeStatus.Status;
                details.Add(toolLifeAttribute);
            }

            return (toolStatus, details);
        }

        public IEnumerable<AttributeDetailItem> GetAttributesInError(IEnumerable<AttributeDetailItem> toolStatusAttributes)
        {
            (_, var details) = GetToolStatusWithDetail(toolStatusAttributes);
            return details;
        }

        public IEnumerable<AttributeDefinitionEnum> GetToolStatusAttributeDefinitions()
        {
            return new List<AttributeDefinitionEnum>
            {
                AttributeDefinitionEnum.MaxNozzleLifeTime,
                AttributeDefinitionEnum.NozzleLifeTime,
                AttributeDefinitionEnum.NozzleLifeAttentionThreshold,
                AttributeDefinitionEnum.ToolEnableA,
                AttributeDefinitionEnum.ToolEnableB,
                AttributeDefinitionEnum.ToolEnableC,
                AttributeDefinitionEnum.ToolEnableD,
            };
        }
    }
}
