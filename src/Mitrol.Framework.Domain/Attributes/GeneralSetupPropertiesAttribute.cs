
namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;
    using System.Collections.Generic;
    using System.Text;

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class GeneralSetupPropertiesAttribute: Attribute
    {
        public AttributeDefinitionEnum AttributeDefinition { get; set; }
        public AttributeDefinitionGroupEnum Group { get; set; }
        public int AttributeOrderNumber { get; set; }
        public GeneralSetupPropertiesAttribute(AttributeDefinitionEnum attribute = 0,
                                               AttributeDefinitionGroupEnum group = AttributeDefinitionGroupEnum.Generic,
                                               int attributeOrder = 0)
        {
            AttributeDefinition = attribute;
            Group = group;
            AttributeOrderNumber = attributeOrder;
        }
    }

}
