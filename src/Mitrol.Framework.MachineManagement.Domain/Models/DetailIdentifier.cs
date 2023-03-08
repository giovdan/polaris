namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Text.Json.Serialization;

    [Table("DetailIdentifier")]
    public class DetailIdentifier : AuditableEntity
    {
        [Required()]
        /// <summary>
        /// Definizione Attributo
        /// </summary>
        public long AttributeDefinitionId { get; set; }
        [Required()]
        public long MasterId { get; set; }
        [Required()]
        public string Value { get; set; }
        [Required()]
        public ParentTypeEnum ParentTypeId { get; set; }
        [Required()]
        public int Priority { get; set; }

        public virtual AttributeDefinition AttributeDefinition { get; set; }
        [JsonIgnore]
        public virtual MasterIdentifier Master {get;set;}
    }

    public static class DetailIdentifierExtensions
    {
        public static string GetValue(this DetailIdentifier detailIdentifier, MeasurementSystemEnum conversionSystem)
        {
            (decimal value, string textValue) = GetInnerValue(detailIdentifier, conversionSystem);

            var attributeValue = new AttributeValue
            {
                AttributeDefinition = detailIdentifier.AttributeDefinition,
                AttributeDefinitionId = detailIdentifier.AttributeDefinitionId,
                Value = value,
                TextValue = textValue,
                DataFormatId = (AttributeDataFormatEnum)detailIdentifier.AttributeDefinition.DataFormatId,
                ParentTypeId = detailIdentifier.ParentTypeId,
                SubParentTypeId = detailIdentifier.Master.ParentId
            };
            
            return attributeValue.GetAttributeValue(conversionSystem).ToString();
        }

        private static (decimal value, string textValue) GetInnerValue(DetailIdentifier detailIdentifier
            , MeasurementSystemEnum conversionSystem)
        {
            decimal decimalValue = 0;
            string textValue = string.Empty;

            switch (detailIdentifier.AttributeDefinition.AttributeKindId)
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
