namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Enums;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ToolDetailItem : ToolListItem, IEntityWithImage, IHasAttributes
    {
        [JsonProperty("NumberOfCopies")]
        public int NumberOfCopies { get; set; }

        [JsonProperty("ImageCode")]
        public string ImageCode { get; set; }

        [JsonProperty("UnitEnablingMask")]
        public ToolUnitMaskEnum UnitEnablingMask { get; set; }

        [JsonProperty("Unit")]
        /// <summary>
        /// Numero di unità
        /// </summary>
        public UnitEnum Unit { get; set; }

        [JsonProperty("Slot")]
        /// <summary>
        /// Numero di slot del magazzino unità
        /// </summary>
        public short Slot { get; set; }

        [JsonProperty("UnitTypeLocalizationKey")]
        public string UnitTypeLocalizationKey { get; set; }

        [JsonProperty("ToolRangeCount")]
        public int ToolRangeCount { get; set; } //Numero di righe per il tool

        [JsonProperty("MachineName")]
        public string MachineName { get; set; }

        [JsonProperty("PlantName")]
        public string PlantName { get; set; }

        [JsonIgnore()]
        public override PlantUnitEnum PlantUnit
            => DomainExtensions.GetEnumAttribute<ToolTypeEnum, PlantUnitAttribute>(ToolType)?.PlantUnit ?? PlantUnitEnum.None;

        [JsonProperty("Identifiers")]
        public List<AttributeDetailItem> Identifiers { get; set; }

        [JsonProperty("Attributes")]
        public List<AttributeDetailItem> Attributes { get; set; }

        [JsonProperty("LifeColor")]
        public StatusColorEnum LifeColor { get; set; }

        [JsonProperty("ConversionSystem")]
        public MeasurementSystemEnum ConversionSystem { get; set; }

        [JsonIgnore()]
        public long WarehouseId { get; set; }

        [JsonIgnore()]
        public string LocalizationCode { get; internal set; }

        [JsonIgnore()]
        public UpdateSourceEnum Source { get; set; }

        public ToolDetailItem():base()
        {
            Identifiers = new List<AttributeDetailItem>();
            Attributes = new List<AttributeDetailItem>();
            Source = UpdateSourceEnum.Application;
        }

        public override string Code
        {
            get
            {
                //Costruisce il codice univoco del tool basandosi sugli attributi "identificatori"
                //Recupera l'atttributo Internal Code o DisplayName per il toolType
                var code = s_toolTypeEnumTypeConverter.ConvertToString(ToolType);
                return $"{code} - {MachineManagementExtensions.ToString(CodeGenerators)}";
            }
        }
    }

    public static class ToolDetailItemExtensions
    {
        public static IEnumerable<EntityTypeEnum> GetToolEntityTypes()
        {
            return new List<EntityTypeEnum>
            {
                EntityTypeEnum.ToolTS15,
                EntityTypeEnum.ToolTS16,
                EntityTypeEnum.ToolTS17,
                EntityTypeEnum.ToolTS18,
                EntityTypeEnum.ToolTS19,
                EntityTypeEnum.ToolTS20,
                EntityTypeEnum.ToolTS32,
                EntityTypeEnum.ToolTS33,
                EntityTypeEnum.ToolTS34,
                EntityTypeEnum.ToolTS35,
                EntityTypeEnum.ToolTS36,
                EntityTypeEnum.ToolTS38,
                EntityTypeEnum.ToolTS39,
                EntityTypeEnum.ToolTS40,
                EntityTypeEnum.ToolTS41,
                EntityTypeEnum.ToolTS50,
                EntityTypeEnum.ToolTS51,
                EntityTypeEnum.ToolTS52,
                EntityTypeEnum.ToolTS53,
                EntityTypeEnum.ToolTS54,
                EntityTypeEnum.ToolTS55,
                EntityTypeEnum.ToolTS56,
                EntityTypeEnum.ToolTS57,
                EntityTypeEnum.ToolTS61,
                EntityTypeEnum.ToolTS62,
                EntityTypeEnum.ToolTS68,
                EntityTypeEnum.ToolTS69,
                EntityTypeEnum.ToolTS70,
                EntityTypeEnum.ToolTS71,
                EntityTypeEnum.ToolTS73,
                EntityTypeEnum.ToolTS74,
                EntityTypeEnum.ToolTS75,
                EntityTypeEnum.ToolTS76,
                EntityTypeEnum.ToolTS77,
                EntityTypeEnum.ToolTS78,
                EntityTypeEnum.ToolTS79,
                EntityTypeEnum.ToolTS80,
                EntityTypeEnum.ToolTS87,
                EntityTypeEnum.ToolTS88,
                EntityTypeEnum.ToolTS89,
                EntityTypeEnum.ToolTS51XPR,
                EntityTypeEnum.ToolTS51HPR,
                EntityTypeEnum.ToolTS53XPR,
                EntityTypeEnum.ToolTS53HPR
            };
        }

        /// <summary>
        /// Get Attribute Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tool"></param>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public static T  GetAttributeValue<T>(this ToolDetailItem tool, AttributeDefinitionEnum enumId)
        {
            var attribute = tool.Identifiers.SingleOrDefault(
                              id => id.EnumId == enumId);

            if (attribute == null)
            {
                return default(T);
            }

            var attributeValue = attribute.GetAttributeValue();
            if (attributeValue is T)
            {
                return (T)attributeValue;
            }
            try
            {
                return (T)Convert.ChangeType(attributeValue, typeof(T));
            }
            catch (InvalidCastException)
            {
                return default(T);
            }

        }

        private static IEnumerable<ToolStatusAttribute> GetToolStatusAttributes(this ToolDetailItem toolDetail
            , IToolStatus toolStatusHandler)
        {
            throw new NotImplementedException();
            // Recupero gli attributi di stato legati al tipo di tool (stabiliti dal toolStatusHandler)
            //var toolStatusAttributeDefinitions = toolStatusHandler.GetToolStatusAttributeDefinitions();

            //return toolDetail.Attributes.Where(a => toolStatusAttributeDefinitions.Contains(a.EnumId))
            //            .Select(a =>
            //            {
            //                var attributeValue = a.Value.GetAttributeValue(a.AttributeKind, a.ItemDataFormat, a.TypeName,
            //                                    toolDetail.ConversionSystem);

            //                var toolStatusAttribute = new ToolStatusAttribute
            //                {
            //                    AttributeTypeId = a.AttributeType,
            //                    AttributeKindId = a.AttributeKind,
            //                    ControlTypeId = a.ControlType,
            //                    DataFormatId = a.ItemDataFormat,
            //                    DisplayName = a.DisplayName,
            //                    EnumId = a.EnumId,
            //                    GroupId = a.GroupId,
            //                    Id = a.Id,
            //                    ProtectionLevel = a.ProtectionLevel,
            //                    EntityId = 0
            //                    PlantUnitId = toolDetail.PlantUnit,
            //                    Priority = a.Order,
            //                    TextValue = string.Empty,
            //                    Value = 0
            //                };

            //                if (a.AttributeKind == AttributeKindEnum.String)
            //                {
            //                    toolStatusAttribute.TextValue = attributeValue.ToString();
            //                }
            //                else 
            //                {
            //                    toolStatusAttribute.Value = Convert.ToDecimal(attributeValue);
            //                }

            //                return toolStatusAttribute;
            //            });
        }

        /// <summary>
        /// Set tool detail status based on some rules
        /// </summary>
        /// <param name="tool"></param>
        /// <param name="toolStatusHandler"></param>
        public static void SetToolStatus(this ToolDetailItem tool
                        , IToolStatus toolStatusHandler)
        {
            if (toolStatusHandler != null)
            {
                // Setto lo stato del tool
                (tool.Status, tool.StatusLocalizationKey) = toolStatusHandler.GetToolStatus(tool.Attributes);
                (tool.Percentage, tool.LifeColor) = toolStatusHandler.GetBatteryStatus(tool.GetToolStatusAttributes(toolStatusHandler));

                var attributesInError = toolStatusHandler.GetAttributesInError(tool.Attributes).ToList();
                if (attributesInError != null)
                {
                    tool.Attributes = tool.Attributes.Select(attr =>
                    {
                        var attributeToModify = attributesInError.SingleOrDefault(a => a.EnumId == attr.EnumId);
                        if (attributeToModify != null)
                        {
                            attr.AttributeStatus = attributeToModify.AttributeStatus;
                        }
                        return attr;
                    }
                    ).ToList();
                }

            }
        }
    }
}