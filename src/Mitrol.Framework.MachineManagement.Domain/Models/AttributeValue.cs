namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models.Database;
    using Mitrol.Framework.Domain.Enums;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AttributeValue")]
    public class AttributeValue: BaseAuditableEntityWithRowVersion
    {
        public long AttributeDefinitionId { get; set; }
        public long EntityId { get; set; }
        public decimal? Value { get; set; }
        public string TextValue { get; set; }
        [Required()]
        public AttributeDataFormatEnum DataFormat { get; set; }
        [DefaultValue(0)]
        public int Priority { get; set; }

        public virtual AttributeDefinition AttributeDefinition { get; set; }
        public virtual Entity Entity { get; set; }
    }

    //public static class AttributeValueExtensions
    //{
    //    public static AttributeValue Clone(this AttributeValue attributeValue, long parentId
    //        , MeasurementSystemEnum measurementSystemFrom
    //        , MeasurementSystemEnum measurementSystemTo = MeasurementSystemEnum.MetricSystem)
    //    {
    //        //var newAttribute = Cloner<AttributeValue>.Clone(attributeValue);
    //        var newAttribute = attributeValue.DeepClone();
    //        newAttribute.Id = 0;
    //        newAttribute.ParentId = parentId;
    //        newAttribute.AttributeDefinition = null;
    //        if (measurementSystemFrom != measurementSystemTo &&
    //            attributeValue.AttributeDefinition.AttributeKind == AttributeKindEnum.Number)
    //        {
    //            newAttribute.Value = ConvertToHelper.Convert(conversionSystemFrom: measurementSystemFrom
    //                            , conversionSystemTo: measurementSystemTo
    //                            , attributeValue.DataFormat
    //                            , attributeValue.Value.GetValueOrDefault(0))?.Value ?? attributeValue.Value;
    //        }
    //        return newAttribute;
    //    }

    //    public static object GetAttributeValue(this AttributeValue attributeValue, MeasurementSystemEnum conversionSystem)
    //    {
    //        object value = null;

    //        switch (attributeValue.AttributeDefinition.AttributeKindId)
    //        {
    //            case AttributeKindEnum.Enum:
    //                {
    //                    if (!string.IsNullOrEmpty(attributeValue.AttributeDefinition.TypeName))
    //                    {
    //                        var enumType = Type.GetType(attributeValue.AttributeDefinition.TypeName);
    //                        var converter = TypeDescriptor.GetConverter(enumType);
    //                        value = converter.ConvertFrom(attributeValue.Value);
    //                    }
    //                    else
    //                    {
    //                        value = attributeValue.Value;
    //                    }
    //                }
    //                break;
    //            case AttributeKindEnum.Number:
    //                {
    //                    if (decimal.TryParse(attributeValue.Value.ToString(), out var decimalValue))
    //                    {
    //                        value = ConvertToHelper.Convert(
    //                                conversionSystemFrom: MeasurementSystemEnum.MetricSystem
    //                                , conversionSystemTo: conversionSystem
    //                                , dataFormat: (AttributeDataFormatEnum)attributeValue.AttributeDefinition.DataFormatId
    //                                , decimalValue
    //                                , true)?.Value ?? attributeValue.Value;
    //                    }
    //                    else
    //                        value = attributeValue.Value;
    //                }
    //                break;
    //            case AttributeKindEnum.String:
    //                value = attributeValue.TextValue;
    //                break;
    //            default:
    //                value = attributeValue.Value;
    //                break;
    //        }
    //        return value;
    //    }

    //    /// <summary>
    //    /// Get Attribute Value from AttributeValueItem entity
    //    /// </summary>
    //    /// <param name="attributeValue"></param>
    //    /// <param name="attributeKind"></param>
    //    /// <param name="attributeDataFormat"></param>
    //    /// <param name="typeName"></param>
    //    /// <param name="conversionSystem"></param>
    //    /// <returns></returns>
    //    public static object GetAttributeValue(this AttributeValueItem attributeValue
    //                , AttributeKindEnum attributeKind
    //                , AttributeDataFormatEnum attributeDataFormat
    //                , string typeName
    //                , MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem)
    //    {
    //        object value = null;

    //        switch (attributeKind)
    //        {
    //            case AttributeKindEnum.Enum:
    //                {
    //                    if (!string.IsNullOrEmpty(typeName))
    //                    {
    //                        var enumType = Type.GetType(typeName);
    //                        var converter = TypeDescriptor.GetConverter(enumType);
    //                        value = converter.ConvertFrom(attributeValue.CurrentValueId);
    //                    }
    //                    else
    //                    {
    //                        value = attributeValue.CurrentValue != null
    //                            ? (object)attributeValue.CurrentValue
    //                            : attributeValue.CurrentValueId;
    //                    }
    //                }
    //                break;
    //            case AttributeKindEnum.Number:
    //                {
    //                    if (decimal.TryParse(attributeValue.CurrentValue.ToString(), out var decimalValue))
    //                    {
    //                        value = ConvertToHelper.Convert(
    //                                conversionSystemFrom: MeasurementSystemEnum.MetricSystem
    //                                , conversionSystemTo: conversionSystem
    //                                , dataFormat: attributeDataFormat
    //                                , decimalValue
    //                                , true)?.Value ?? attributeValue.CurrentValue;
    //                    }
    //                    else
    //                        value = attributeValue.CurrentValue;

    //                }
    //                break;
    //            case AttributeKindEnum.String:
    //                value = attributeValue.CurrentValue;
    //                break;
    //            default:
    //                value = attributeValue.CurrentValueId > 0 ? (object)attributeValue.CurrentValueId
    //                                : attributeValue.CurrentValue;
    //                break;

    //        }

    //        return value;
    //    }
    //}
}
