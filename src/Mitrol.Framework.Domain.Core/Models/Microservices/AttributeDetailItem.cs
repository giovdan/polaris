namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Conversions;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;

    public class AttributeDetailItemComparer : IEqualityComparer<AttributeDetailItem>
    {
        public static AttributeDetailItemComparer Default => s_comparer.Value;

        private static readonly Lazy<AttributeDetailItemComparer> s_comparer
            = new Lazy<AttributeDetailItemComparer>(Creator);

        private static AttributeDetailItemComparer Creator() => new AttributeDetailItemComparer();

        public bool Equals(AttributeDetailItem first, AttributeDetailItem second)
        {
#pragma warning disable IDE0041
            var is_first_null = ReferenceEquals(first, null);
            var is_second_null = ReferenceEquals(second, null);
#pragma warning restore IDE0041

            if (is_first_null && is_second_null)
                return true;
            else if (is_first_null || is_second_null)
                return false;
            else
            {
                return first.AttributeDefinitionLinkId == second.AttributeDefinitionLinkId;
            }
        }

        public int GetHashCode(AttributeDetailItem obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var hashCode = 792638326;

            hashCode = hashCode * -1521134295 + EqualityComparer<long>.Default.GetHashCode(obj.AttributeDefinitionLinkId);

            return hashCode;
        }
    }

    public class AttributeDetailItem: DetailItem<AttributeValueItem>, IConvertable
    {
        [JsonProperty("UMLocalizationKey")]
        public string UMLocalizationKey { get; set; }

        [JsonProperty("DecimalPrecision")]
        public int DecimalPrecision { get; set; }

        [JsonProperty("AttributeType")]
        public AttributeTypeEnum AttributeType { get; set; }

        [JsonProperty("ItemDataFormat")]
        public AttributeDataFormatEnum ItemDataFormat { get; set; }

        [JsonProperty("ControlType")]
        public ClientControlTypeEnum ControlType { get; set; }

        [JsonProperty("AttributeScopeId")]
        public AttributeScopeEnum AttributeScopeId { get; set; }

        [JsonProperty("IsReadonly")]
        public bool IsReadonly { get; set; }

        [JsonIgnore()]
        public bool IsStatusAttribute { get; set; }

        [JsonIgnore()]
        public ProtectionLevelEnum ProtectionLevel { get; set; }

        [JsonIgnore()]
        public object DbValue { get; set; }

        [JsonIgnore()]
        public string TextValue { get; set; }

        [JsonIgnore()]
        public AttributeDefinitionGroupEnum GroupId { get; set; }

        [JsonProperty("IsCodeGenerator")]
        public bool IsCodeGenerator { get; set; }

        [JsonProperty("UsedForGraphicsPreview")]
        public bool UsedForGraphicsPreview { get; set; }

        [JsonProperty("IsFake")]
        public bool IsFake { get; set; }

        [JsonProperty("AttributeStatus")]
        public AttributeStatus AttributeStatus { get; set; }

        [JsonProperty("Hidden")]
        public bool Hidden { get; set; }

        [JsonProperty("LocalizedText")]
        public string LocalizedText { get; set; }


        [JsonProperty("HelpImage")]
        public string HelpImage { get; set; }

        [JsonProperty("UseLastInsertedAsDefault")]
        public bool UseLastInsertedAsDefault { get; set; }



        public AttributeDetailItem()
        {
            Value = new AttributeValueItem();
            IsReadonly = false;
            IsFake = false;
            AttributeStatus = new AttributeStatus();
            Hidden = false;
            LocalizedText = string.Empty;
        }

        public AttributeDetailItem Clone() => this.MemberwiseClone() as AttributeDetailItem;
    }

    /// <summary>
    /// Clustered Attribute Detail Item for secondary level attribute aggregation
    /// </summary>
    public class ClusteredAttributeDetailItem: AttributeDetailItem
    {
        
        public ClusteredAttributeDetailItem()
        {
            EnumId = AttributeDefinitionEnum.None;
            UsedForGraphicsSlaves = new HashSet<DatabaseDisplayNameEnum>();
        }


        [JsonProperty("ClusterName")]
        public string ClusterName { get; set; }
        [JsonProperty("ClusterId")]
        public int ClusterId { get; set; }

        [JsonProperty("ReactToChange")]
        public bool ReactToChange { get; set; }

        [JsonProperty("UsedForGraphicsSlaves")]
        public HashSet<DatabaseDisplayNameEnum> UsedForGraphicsSlaves { get; set; }
    }

    public static class AttributeDetailItemExtensions
    {

        public static void SetFormatForLabel(this AttributeDetailItem attribute, MeasurementSystemEnum conversionSystemTo)
        {
            if (attribute.AttributeKind == AttributeKindEnum.Number && 
                attribute.Value.CurrentValue != null
                && decimal.TryParse(attribute.Value.CurrentValue.ToString(), out var decimalValue))
            {
                var decimalDigitsForLabel = DomainExtensions.GetEnumAttributes<AttributeDataFormatEnum, DecimalPrecisionAttribute>(
                            attribute.ItemDataFormat)?.SingleOrDefault(x => x.SystemOfMeasure == conversionSystemTo)?
                            .NumberOfDigitsForLabel ?? 1;
                attribute.Value.CurrentValue = Math.Round(decimalValue, decimalDigitsForLabel);
            }
        }

        /// <summary>
        /// Converts the value stored in AttributeValue object to the specified conversion system and returns it.
        /// </summary>
        /// <param name="attribute">AttributeDetailItem object to get the value from.</param>
        /// <param name="conversionSystemFrom">The system in which the value is.</param>
        /// <param name="conversionSystemTo">The system in which the value needs to be represented.</param>
        /// <param name="checkTypeNameForEnum">Flag to check TypeName for Enum</param>
        /// <param name="applyRoundForLabel">Flag to check if round is required</param>
        /// <returns>The converted value.</returns>
        public static object GetAttributeValue(this AttributeDetailItem attribute
                    , MeasurementSystemEnum conversionSystemFrom = MeasurementSystemEnum.MetricSystem
                    , MeasurementSystemEnum conversionSystemTo = MeasurementSystemEnum.MetricSystem
                    , bool checkTypeNameForEnum = true
                    , bool applyRoundForLabel = false)
        {
            object value = attribute.Value.CurrentValue;

            switch (attribute.AttributeKind)
            {
                case AttributeKindEnum.Enum:
                    {
                        if (!string.IsNullOrEmpty(attribute.TypeName) && checkTypeNameForEnum)
                        {
                            var enumType = Type.GetType(attribute.TypeName);
                            var converter = TypeDescriptor.GetConverter(enumType);
                            value = (object)converter.ConvertFrom(attribute.Value.CurrentValueId)?.ToString() ?? attribute.Value.CurrentValueId;
                        }
                        else
                        {
                            value = attribute.Value.CurrentValueId;
                        }
                    }
                    break;
                case AttributeKindEnum.Number:
                    {
                        if (value == null)
                        {
                            value = 0;
                        }
                        else
                        {
                            if (decimal.TryParse(attribute.Value.CurrentValue.ToString(), out var decimalValue))
                            {
                                if (applyRoundForLabel)
                                {
                                    value = ConvertToHelper.ConvertForLabel(conversionSystemFrom: conversionSystemFrom
                                        , conversionSystemTo: conversionSystemTo
                                        , dataFormat: attribute.ItemDataFormat
                                        , value: decimalValue).Value;
                                }
                                else
                                {
                                    value = ConvertToHelper.Convert(conversionSystemFrom: conversionSystemFrom
                                        , conversionSystemTo: conversionSystemTo
                                        , dataFormat: attribute.ItemDataFormat
                                        , value: decimalValue
                                        , applyRound: true).Value;
                                }
                            }
                            else
                                value = attribute.Value.CurrentValue;
                        }
                    }
                    break;
                case AttributeKindEnum.String:
                    {
                        if (value == null)
                        {
                            value = string.Empty;
                        }
                    }
                    break;
            }

            return value;
        }

        /// <summary>
        /// Set Attribute Detail Item value
        /// </summary>
        /// <param name="attributeDetail"></param>
        /// <param name="value"></param>
        public static void SetAttributeValue(this AttributeDetailItem attributeDetail, object value
                                            , MeasurementSystemEnum measurementSystem)
        {
            attributeDetail.DbValue = value;
            switch (attributeDetail.AttributeKind)
            {
                case AttributeKindEnum.String:
                case AttributeKindEnum.Number:
                case AttributeKindEnum.Bool:
                    if (attributeDetail.AttributeKind == AttributeKindEnum.Number 
                            && measurementSystem != MeasurementSystemEnum.MetricSystem
                            && decimal.TryParse(value.ToString(), out var decimalValue))
                    {
                        attributeDetail.Value.CurrentValue = ConvertToHelper.Convert(
                                    conversionSystemFrom: MeasurementSystemEnum.MetricSystem,
                                    conversionSystemTo: measurementSystem,
                                    dataFormat: attributeDetail.ItemDataFormat,
                                    value: decimalValue);
                    }
                    else
                    {
                        attributeDetail.Value.CurrentValue = value;
                    }
                    break;
                case AttributeKindEnum.Enum:
                    attributeDetail.Value.CurrentValue = 0;
                    if (int.TryParse(value.ToString(), out var intValue))
                    {
                        attributeDetail.Value.CurrentValueId = intValue;
                    }
                    else if (!string.IsNullOrEmpty(attributeDetail.TypeName))
                    {
                        var enumType = Type.GetType(attributeDetail.TypeName);
                        var converter = TypeDescriptor.GetConverter(enumType);
                        attributeDetail.Value.CurrentValue = converter.ConvertFrom(value);
                    }
                    break;
            }
        }
    }
}