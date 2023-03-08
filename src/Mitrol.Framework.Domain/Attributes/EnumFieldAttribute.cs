namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using System;

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class EnumFieldAttribute :Attribute
    {
        public bool MustBeTranslated { get; set; }

        public string Description { get; set; }

        public string Localization { get; set; }

        public AttributeDefinitionEnum AttributeDefinition { get; set; }
        public EnumFieldAttribute(string description
                                , bool mustBeTraslated
                                , string localization
                                , AttributeDefinitionEnum attribute=0) 
        {
            MustBeTranslated = mustBeTraslated;
            Description = description;
            Localization = localization;
            AttributeDefinition = attribute;
        }
    }
}
