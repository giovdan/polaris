namespace Mitrol.Framework.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;
    using System.ComponentModel;

    public interface IImportItem
    {
        Dictionary<string, object> Identifiers { get; set; }
        
        Dictionary<string, object> Attributes { get; set; }

        TypeConverter AttributeDefinitionConverter { get; set; }
        void Convert(JObject jobject);
        string GetIdentifiersString();
        void AddAttribute(AttributeDefinitionEnum enumId, object value);

        void AddIdentifier(AttributeDefinitionEnum enumId, object value);

    }
}
