namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Attributes;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
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
