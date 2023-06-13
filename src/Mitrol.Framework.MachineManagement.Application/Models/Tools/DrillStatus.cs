namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class AttributeStatusExtensions
    {
        public static AttributeStatus SetAttributeStatus(decimal toolLifeMaxValue, decimal toolLifeValue, decimal warningLifeValue)
        {
            AttributeStatus attributeStatus = new AttributeStatus
            {
                Status = EntityStatusEnum.Available
            };

            // I controlli sulla vita utensile devono essere effettuati solo se specificata la 
            // massima vita utensile.
            // Se la vita utensile è >= vita massima => Allarme
            // Altrimenti se la vita utensile è >= warning Life => Warning
            // Altrimenti => disponibile
            if (toolLifeMaxValue > 0)
            {
                if (toolLifeValue >= toolLifeMaxValue)
                {
                    attributeStatus.ErrorLocalizationKey = $"{MachineManagementExtensions.LABEL_TOOLSTATUS}_{"EXHAUSTED"}";
                    attributeStatus.Status = EntityStatusEnum.Alarm;
                }
                else if (warningLifeValue > 0 && toolLifeValue >= warningLifeValue)
                {
                    attributeStatus.ErrorLocalizationKey = $"{MachineManagementExtensions.LABEL_TOOLSTATUS}_{"EXHAUSTING"}";
                    attributeStatus.Status = EntityStatusEnum.Warning;

                }
            }

            return attributeStatus;
        }
    }

    public class DrillStatus : IToolStatus
    {
        private IMachineParameterService ParameterService => ServiceFactory.GetService<IMachineParameterService>();
        private IExecutionService ExecutionService => ServiceFactory.GetService<IExecutionService>();

        public IServiceFactory ServiceFactory { get; set; }

        public DrillStatus(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
        }

        public (int? Percentage, StatusColorEnum StatusColor) GetBatteryStatus(IEnumerable<ToolStatusAttribute> toolStatusAttributes)
        {
            var toolLifeMax = toolStatusAttributes.SingleOrDefault
                  (a => a.EnumId == AttributeDefinitionEnum.MaxToolLife);

            var toolLife = toolStatusAttributes.SingleOrDefault
                (a => a.EnumId == AttributeDefinitionEnum.ToolLife);

            var warningLife = toolStatusAttributes.SingleOrDefault
                (a => a.EnumId == AttributeDefinitionEnum.WarningToolLife);

            // Recupero gli attributi legati alla massima vita ed alla vita corrente
            if (toolLifeMax == null || toolLife == null)
                return (100, StatusColorEnum.Grey);

            // se warningLife è definito ma è 0 allora passo toolLifeMax 
            // se warningLife non è definito allora passo toolLifeMax
            // warningLife è definito ed è diverso da 0 allora passo warningLife
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
            var toolStatus = ExecutionService.CheckIfToolIsUnitEnabled(toolStatusAttributes, PlantUnitEnum.DrillingMachine);

            var toolLifeMaxAttribute = toolStatusAttributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.MaxToolLife);
            // Caso particolare TS57 (Segatrice con lama rotante) => Gitea #425
            if (toolLifeMaxAttribute == null)
            {
                toolLifeMaxAttribute = toolStatusAttributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.MaxBladeLife);
            }

            var toolLifeMaxValue = Convert.ToDecimal(toolLifeMaxAttribute.Value.CurrentValue);

            var toolLifeAttribute = toolStatusAttributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.ToolLife);
            // Caso particolare TS57 (Segatrice con lama rotante) => Gitea #425
            if (toolLifeAttribute == null)
            {
                toolLifeAttribute = toolStatusAttributes.SingleOrDefault(a => a.EnumId 
                            == AttributeDefinitionEnum.BladeLife);
            }
            var toolLifeValue = Convert.ToDecimal(toolLifeAttribute.Value.CurrentValue);

            var warningLifeAttribute = toolStatusAttributes.SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.WarningToolLife);
            // Caso particolare TS57(Segatrice con lama rotante) => Gitea #425
            if (warningLifeAttribute == null)
            {
                warningLifeAttribute = toolStatusAttributes.SingleOrDefault(a => a.EnumId
                            == AttributeDefinitionEnum.WarningBladeLife);
            }
            var warningLifeValue = Convert.ToDecimal(warningLifeAttribute.Value.CurrentValue);

            toolLifeAttribute.AttributeStatus = AttributeStatusExtensions.SetAttributeStatus(toolLifeMaxValue, toolLifeValue, warningLifeValue);
            if (toolLifeAttribute.AttributeStatus.Status != EntityStatusEnum.Available)
            {
                toolStatus = toolLifeAttribute.AttributeStatus.Status;
                details.Add(toolLifeAttribute);
            }

            var MisuraPuntaAttribute = toolStatusAttributes.FirstOrDefault(x => x.EnumId == AttributeDefinitionEnum.AutoSensitiveEnable);
            if (MisuraPuntaAttribute != null && Convert.ToBoolean(MisuraPuntaAttribute.Value.CurrentValue))
            {
                toolStatus = EntityStatusEnum.Warning;
                MisuraPuntaAttribute.AttributeStatus.ErrorLocalizationKey = $"{MachineManagementExtensions.LABEL_TOOLSTATUS}_{"AUTOSENSITIVEENABLED"}";
                MisuraPuntaAttribute.AttributeStatus.Status = toolStatus;
                details.Add(MisuraPuntaAttribute);
            }

            var Param_V650 = ParameterService.Get(ParameterCodes.PAR_V650_1)?.Value ?? 0;
            var lenghtAttribute = toolStatusAttributes.FirstOrDefault(x => x.EnumId == AttributeDefinitionEnum.ToolLength);
            if (lenghtAttribute != null && Param_V650 > 0 && Convert.ToDecimal(lenghtAttribute.Value.CurrentValue) > Param_V650)
            {
                toolStatus = EntityStatusEnum.Alarm;
                lenghtAttribute.AttributeStatus.ErrorLocalizationKey = $"{MachineManagementExtensions.LABEL_TOOLSTATUS}_{"TOOLLENGTH"}";
                lenghtAttribute.AttributeStatus.Status = toolStatus;
                details.Add(lenghtAttribute);
            }

            return (toolStatus, details);
        }
        public IEnumerable<AttributeDetailItem> GetAttributesInError(IEnumerable<AttributeDetailItem> toolStatusAttributes)
        {
            (_, var details) = GetToolStatusWithDetail(toolStatusAttributes);
            return details;
        }

        /// <summary>
        /// Recupera la lista degli attributi di stato 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AttributeDefinitionEnum> GetToolStatusAttributeDefinitions()
        {
            return new List<AttributeDefinitionEnum>
            {
                AttributeDefinitionEnum.MaxToolLife,
                AttributeDefinitionEnum.ToolLife,
                AttributeDefinitionEnum.WarningToolLife,
                AttributeDefinitionEnum.ToolLength,
                AttributeDefinitionEnum.AutoSensitiveEnable,
                AttributeDefinitionEnum.ToolEnableA,
                AttributeDefinitionEnum.ToolEnableB,
                AttributeDefinitionEnum.ToolEnableC,
                AttributeDefinitionEnum.ToolEnableD,
            };
        }
    }
}
