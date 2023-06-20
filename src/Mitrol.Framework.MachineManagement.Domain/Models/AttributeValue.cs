namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Conversions;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Core.Models.Database;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AttributeValue")]
    public class AttributeValue : BaseAuditableEntityWithRowVersion
    {
        [Required()]
        public long AttributeDefinitionLinkId { get; set; }
        [Required()]
        public long EntityId { get; set; }
        public decimal? Value { get; set; }
        public string TextValue { get; set; }
        public int Priority { get; set; }
        public AttributeDataFormatEnum DataFormatId { get; set; }

        public virtual AttributeDefinitionLink AttributeDefinitionLink { get; set; }
        public virtual Entity Entity { get; set; }
    }

    public static class AttributeValueExtensions
    {
        public static AttributeValue Clone(this AttributeValue attributeValue, long entityId
            , MeasurementSystemEnum measurementSystemFrom
            , MeasurementSystemEnum measurementSystemTo = MeasurementSystemEnum.MetricSystem)
        {
            //var newAttribute = Cloner<AttributeValue>.Clone(attributeValue);
            var newAttribute = attributeValue.DeepClone();
            newAttribute.Id = 0;
            newAttribute.EntityId = entityId;
            newAttribute.AttributeDefinitionLink = null;
            if (measurementSystemFrom != measurementSystemTo &&
                attributeValue.AttributeDefinitionLink.AttributeDefinition.AttributeKind == AttributeKindEnum.Number)
            {
                newAttribute.Value = ConvertToHelper.Convert(conversionSystemFrom: measurementSystemFrom
                                , conversionSystemTo: measurementSystemTo
                                , attributeValue.DataFormatId
                                , attributeValue.Value.GetValueOrDefault(0))?.Value ?? attributeValue.Value;
            }
            return newAttribute;
        }

        public static object GetAttributeValue(this AttributeValue attributeValue
                , MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem)
        {
            object value = null;

            switch (attributeValue.AttributeDefinitionLink.AttributeDefinition.AttributeKind)
            {
                case AttributeKindEnum.Enum:
                    {
                        if (!string.IsNullOrEmpty(attributeValue.AttributeDefinitionLink.AttributeDefinition.TypeName))
                        {
                            var enumType = Type.GetType(attributeValue.AttributeDefinitionLink.AttributeDefinition.TypeName);
                            var converter = TypeDescriptor.GetConverter(enumType);
                            value = converter.ConvertFrom(attributeValue.Value);
                        }
                        else
                        {
                            value = attributeValue.Value;
                        }
                    }
                    break;
                case AttributeKindEnum.Number:
                    {
                        if (decimal.TryParse(attributeValue.Value.ToString(), out var decimalValue))
                        {
                            value = ConvertToHelper.Convert(
                                    conversionSystemFrom: MeasurementSystemEnum.MetricSystem
                                    , conversionSystemTo: conversionSystem
                                    , dataFormat: attributeValue.AttributeDefinitionLink
                                                        .AttributeDefinition.DataFormat
                                    , decimalValue
                                    , true)?.Value ?? attributeValue.Value;
                        }
                        else
                            value = attributeValue.Value;
                    }
                    break;
                case AttributeKindEnum.String:
                    value = attributeValue.TextValue;
                    break;
                default:
                    value = attributeValue.Value;
                    break;
            }
            return value;
        }

    }
}
