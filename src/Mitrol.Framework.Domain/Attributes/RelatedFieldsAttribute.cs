namespace Mitrol.Framework.MachineManagement.Domain.Enums
{
    using System;
    using System.Collections.Generic;
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class RelatedFieldsAttribute : Attribute
    {
        public IEnumerable<AttributeDefinitionEnum> Fields { get; set; }
        public RelatedFieldsAttribute(params AttributeDefinitionEnum[] attributeDefinitions)
        {
            Fields = attributeDefinitions;
        }
    }
}