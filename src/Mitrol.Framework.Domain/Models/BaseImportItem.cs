using Mitrol.Framework.Domain.Enums;
using Mitrol.Framework.Domain.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;

namespace Mitrol.Framework.Domain.Models
{
    public class BaseImportItem : IImportItem
    {
        [JsonIgnore]
        public virtual List<KeyValuePair<ImportExportObjectEnum,long>> Dependencies { get; set; }

        [JsonIgnore]
        public virtual Dictionary<string, object> Identifiers { get; set; }

        [JsonProperty("Attributes")]
        public virtual Dictionary<string, object> Attributes { get; set; }

        [JsonIgnore]
        public virtual TypeConverter AttributeDefinitionConverter { get; set; }
    

        public BaseImportItem()
        {
            Identifiers = new Dictionary<string, object>();
            Attributes = new Dictionary<string, object>();
            AttributeDefinitionConverter = TypeDescriptor.GetConverter(typeof(AttributeDefinitionEnum));
            Dependencies = new List<KeyValuePair<ImportExportObjectEnum, long>>();
        }

        public virtual void Convert(JObject jobject)
        {
            throw new NotImplementedException();
        }

        public virtual string GetIdentifiersString()
        {
            var stringIdentifiers = string.Empty;
            foreach (var identifier in Identifiers)
            {
                stringIdentifiers += identifier.Value.ToString() + "-";
            }
            stringIdentifiers = stringIdentifiers.Remove(stringIdentifiers.Length - 1);
            return stringIdentifiers;
        }

        public virtual void AddAttribute(AttributeDefinitionEnum enumId, object value)
        {
            //Recupero il nome dell'attributo da esportare: sarà un EnumcustomNameAttribute se definito altrimenti sarà il DisplayName 
            string displayNameToExport = AttributeDefinitionConverter.ConvertTo(null
                                            , CultureInfo.InvariantCulture
                                            , enumId
                                            , typeof(string))
                                            .ToString();
            Attributes.Add(displayNameToExport, value);
        }

        public virtual void AddIdentifier(AttributeDefinitionEnum enumId, object value)
        {
            //Recupero il nome dell'attributo da esportare: sarà un EnumcustomNameAttribute se definito altrimenti sarà il DisplayName 
            string displayNameToExport = AttributeDefinitionConverter.ConvertTo(null
                                            , CultureInfo.InvariantCulture
                                            , enumId
                                            , typeof(string))
                                            .ToString();
            Identifiers.Add(displayNameToExport, value);
        }
    }
}
