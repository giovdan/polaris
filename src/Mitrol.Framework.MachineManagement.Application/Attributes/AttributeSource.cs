namespace Mitrol.Framework.MachineManagement.Application.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    public class AttributeSource
    {
        public string Code { get; set; }
        public AttributeDefinitionEnum EnumId { get; set; }
        public string Value { get; set; }
        public string LocalizationKey { get; set; }
        public string Description { get; set; }
        //Flag che indica se l'insieme dei valori deve essere recuperato da un interrogazione via web api
        //Se true => Value è l'url di interrogazione web api
        //public bool IsDynamicType { get; set; } 
        public bool MustBeTranslated { get; set; }
        public bool IsDefaultValue { get; set; }

        public AttributeSource()
        {
            IsDefaultValue = false;
            MustBeTranslated = true;
        }
    }
}
