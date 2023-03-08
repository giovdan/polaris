namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;


    public class AttributeInfoAttribute: Attribute
    {
        public AttributeInfoAttribute(EntityTypeEnum entityType, AttributeKindEnum attributeKind)
        {
            EntityType = entityType;
            AttributeKind = attributeKind;
        }

        public EntityTypeEnum EntityType { get; set; }
        public AttributeKindEnum AttributeKind { get; set; }
    }
}
