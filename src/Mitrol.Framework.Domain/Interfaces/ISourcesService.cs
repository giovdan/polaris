namespace Mitrol.Framework.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using System.Collections.Generic;

    public interface IAttributeDefinitionEnumManagement
    {
        AttributeDefinitionEnum AttributeDefinition { get; set; }
        void SetAttributeDefinition(AttributeDefinitionEnum attributeDefinition);
        void Init(Dictionary<AttributeDefinitionEnum, object> additionalInfo);
        IEnumerable<AttributeSource> FindAttributeSourceValues();
        AttributeSource GetDefaultValue();
        object GetNameToExportFromValue(BaseInfoItem<long,string> value);
        string GetEnumValueFromSerializationName(string serializationName);
        ValueTypeEnum GetValueType();
        // Recupera il corrispondente valore dell'enumerato dalla sua string
        object GetEnumFromStringValue(string enumInString);
        object GetEnumValueFromAttributeValue(BaseInfoItem<long, string> value);
    }


}
