namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using HotChocolate;
    using HotChocolate.Types;
    using Mitrol.Framework.Domain.Conversions;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using System;
    using System.ComponentModel;

    public class AttributeItem
    {
        [GraphQLName("Id")]
        public long Id { get; set; }
        [GraphQLName("AttributeDefinitionId")]
        public long AttributeDefinitionId { get; set; }
        [GraphQLName("AttributeKind")]
        public AttributeKindEnum AttributeKind { get; set; }
        [GraphQLName("EnumId")]
        public AttributeDefinitionEnum EnumId { get; set; }
        [GraphQLName("Value")]
        public AttributeItemValue Value { get; set; }

        public AttributeItem()
        {
            Value = new AttributeItemValue();
        }
    }


    public class AttributeItemValue
    {
        [GraphQLType(typeof(AnyType))]
        [GraphQLName("CurrentValue")]
        public object CurrentValue { get; set; }
        [GraphQLName("CurrentValueId")]
        public int CurrentValueId { get; set; }
    }

    public static class AttributeItemExtensions
    {

        /// <summary>
        /// Get Attribute Value from AttributeValueItem entity
        /// </summary>
        /// <param name="attributeValue"></param>
        /// <param name="attributeKind"></param>
        /// <param name="attributeDataFormat"></param>
        /// <param name="typeName"></param>
        /// <param name="conversionSystem"></param>
        /// <returns></returns>
        public static object GetAttributeValue(this AttributeItemValue attributeValue
                    , AttributeKindEnum attributeKind
                    , AttributeDataFormatEnum attributeDataFormat
                    , string typeName
                    , MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem)
        {
            object value = null;

            switch (attributeKind)
            {
                case AttributeKindEnum.Enum:
                    {
                        if (!string.IsNullOrEmpty(typeName))
                        {
                            var enumType = Type.GetType(typeName);
                            var converter = TypeDescriptor.GetConverter(enumType);
                            value = converter.ConvertFrom(attributeValue.CurrentValueId);
                        }
                        else
                        {
                            value = attributeValue.CurrentValue != null
                                ? (object)attributeValue.CurrentValue
                                : attributeValue.CurrentValueId;
                        }
                    }
                    break;
                case AttributeKindEnum.Number:
                    {
                        if (decimal.TryParse(attributeValue.CurrentValue.ToString(), out var decimalValue))
                        {
                            value = ConvertToHelper.Convert(
                                    conversionSystemFrom: MeasurementSystemEnum.MetricSystem
                                    , conversionSystemTo: conversionSystem
                                    , dataFormat: attributeDataFormat
                                    , decimalValue
                                    , true)?.Value ?? attributeValue.CurrentValue;
                        }
                        else
                            value = attributeValue.CurrentValue;

                    }
                    break;
                case AttributeKindEnum.String:
                    value = attributeValue.CurrentValue;
                    break;
                default:
                    value = attributeValue.CurrentValueId > 0 ? (object)attributeValue.CurrentValueId
                                    : attributeValue.CurrentValue;
                    break;

            }

            return value;
        }

        public static AttributeValue SetAttributeValue(this AttributeValue dbAttribute, AttributeItem a)
        {
            switch (a.AttributeKind)
            {
                case AttributeKindEnum.Enum:
                    {
                        dbAttribute.Value = a.Value.CurrentValueId;
                        dbAttribute.TextValue = string.Empty;
                    }
                    break;
                case AttributeKindEnum.Bool:
                    {
                        dbAttribute.Value = Convert.ToBoolean(a.Value.CurrentValue) ? 1 : 0;
                        dbAttribute.TextValue = string.Empty;
                    }
                    break;
                case AttributeKindEnum.Number:

                    {
                        dbAttribute.Value = Convert.ToDecimal(a.Value.CurrentValue);
                        dbAttribute.TextValue = string.Empty;
                    }
                    break;
                case AttributeKindEnum.Date:
                case AttributeKindEnum.String:
                    {
                        dbAttribute.TextValue = a.Value.CurrentValue.ToString();
                        dbAttribute.Value = 0;
                    }
                    break;
            }

            return dbAttribute;
        }
    }
}
