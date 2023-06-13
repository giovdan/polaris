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

    public class ToolDetailItem : ToolBaseItem, IEntityWithImage, IHasAttributes
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
        public IEnumerable<AttributeDetailItem> Identifiers { get; set; }

        [JsonProperty("Attributes")]
        public IEnumerable<AttributeDetailItem> Attributes { get; set; }

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

        public string Code { get; set; }
    }

    public static class ToolDetailItemExtensions
    {
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
            var toolStatusAttributeDefinitions = toolStatusHandler.GetToolStatusAttributeDefinitions();

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