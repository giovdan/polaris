namespace RepoDbVsEF.Application.Models
{
    using HotChocolate;
    using HotChocolate.Types;
    using RepoDbVsEF.Domain.Enums;
    using RepoDbVsEF.Domain.Models;
    using System;

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
        public AttributeValueItem Value { get; set; }

        public AttributeItem()
        {
            Value = new AttributeValueItem();
        }
    }

    public class AttributeValueItem
    {
        [GraphQLType(typeof(AnyType))]
        [GraphQLName("CurrentValue")]
        public object CurrentValue { get; set; }
        [GraphQLName("CurrentValueId")]
        public int CurrentValueId { get; set; }
    }

    public static class AttributeItemExtensions
    {
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
