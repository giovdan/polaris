namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    [Table("DetailIdentifier")]
    public class DetailIdentifier : AuditableEntity
    {
        [Required()]
        /// <summary>
        /// Definizione Attributo
        /// </summary>
        public long AttributeDefinitionLinkId { get; set; }
        [Required()]
        public string HashCode { get; set; }
        [Required()]
        public string Value { get; set; }
        [Required()]
        public int Priority { get; set; }

        public virtual AttributeDefinitionLink AttributeDefinitionLink { get; set; }
    }

    public static class DetailIdentifierExtensions
    {
        public static string GetValue(this DetailIdentifier detailIdentifier
                        , long entityId
                        , MeasurementSystemEnum conversionSystem)
        {
            (decimal value, string textValue) = GetInnerValue(detailIdentifier, conversionSystem);

            var attributeValue = new AttributeValue
            {
                AttributeDefinitionLink = detailIdentifier.AttributeDefinitionLink,
                AttributeDefinitionLinkId = detailIdentifier.AttributeDefinitionLinkId,
                Value = value,
                TextValue = textValue,
                DataFormatId = detailIdentifier.AttributeDefinitionLink
                                    .AttributeDefinition.DataFormat,
                EntityId = entityId
            };

            return attributeValue.GetAttributeValue(conversionSystem).ToString();
        }

        private static (decimal value, string textValue) GetInnerValue(
            DetailIdentifier detailIdentifier
            , MeasurementSystemEnum conversionSystem)
        {
            decimal decimalValue = 0;
            string textValue = string.Empty;

            switch (detailIdentifier.AttributeDefinitionLink.AttributeDefinition.AttributeKind)
            {
                case AttributeKindEnum.String:
                    {
                        textValue = detailIdentifier.Value;
                    }
                    break;
                case AttributeKindEnum.Enum:
                    {
                        if (int.TryParse(detailIdentifier.Value, out var enumValue))
                        {
                            decimalValue = enumValue;
                        }
                        else
                        {
                            textValue = detailIdentifier.Value;
                        }
                    }
                    break;
                default:
                    {
                        decimalValue = Convert.ToDecimal(detailIdentifier.Value);
                    }
                    break;
            }

            return (decimalValue, textValue);
        }
    }
}
