namespace Mitrol.Framework.MachineManagement.Application.GeneralPurpose
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Attributes;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Services;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Globalization;
    using System.Linq;

    public class FlatEnumConfigurationManagement : MachineManagementBaseService, IAttributeDefinitionEnumManagement
    {
        protected Dictionary<AttributeDefinitionEnum, object> AdditionalInfo { get; set; }

        public AttributeDefinitionEnum AttributeDefinition { get; set; }

        private AttributeInfoAttribute _attributesInfo;
        public FlatEnumConfigurationManagement(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }

       
        public IEnumerable<AttributeSource> FindAttributeSourceValues()
        {
            var sourceValues = new List<AttributeSource>();
          
            if ( _attributesInfo!=null && _attributesInfo.TypeName != null)
            {
                Type typeEnumerativo = Type.GetType(_attributesInfo.TypeName);
                var typeConverterEnumerativo = TypeDescriptor.GetConverter(typeEnumerativo);
                foreach (var enumVal in typeEnumerativo.GetEnumValues())
                {
                    //converto il valore enumerativo in numero
                    var enumerativeToInt = Convert.ChangeType(enumVal, typeof(int));
                    //trovo le altre proprietà nella definizione dell'attributo
                    var fieldattributes = typeEnumerativo.GetField(enumVal.ToString())?.GetCustomAttributes(typeof(EnumFieldAttribute), false)?
                                                        .Cast<EnumFieldAttribute>()
                                                        .ToDictionary(field => field.AttributeDefinition, field => field) ?? null;
                    if (fieldattributes != null)
                    {
                        var fieldAttribute = fieldattributes.ContainsKey(AttributeDefinition) ?
                                                        fieldattributes[AttributeDefinition] :
                                                        fieldattributes.ContainsKey(0) ? fieldattributes[0] : null;
                        if (fieldAttribute != null)
                        {
                            var attr = new AttributeSource()
                            { 
                                LocalizationKey = fieldAttribute.Localization,
                                MustBeTranslated = fieldAttribute.MustBeTranslated,
                                Description = fieldAttribute.Description,
                                Code = typeEnumerativo.GetEnumName(enumVal),
                                Value = enumerativeToInt.ToString(),
                                EnumId = AttributeDefinition
                            };
                            sourceValues.Add(attr);
                        }
                    }
                }
                //setta il DefaultValue
                DefaultValueAttribute[] attributes = (DefaultValueAttribute[])typeEnumerativo.GetCustomAttributes(typeof(DefaultValueAttribute), false);
                if (attributes != null &&
                    attributes.Length > 0)
                {
                    var value = attributes[0].Value;
                    var def = sourceValues.FirstOrDefault(val => val.Code == value.ToString());
                    if (def != null)
                        def.IsDefaultValue = true;
                }
            }
             
            return sourceValues;
        }

        public AttributeSource GetDefaultValue()
        { 
            var sources = FindAttributeSourceValues();

            if (sources.FirstOrDefault(val => val.IsDefaultValue == true) != null)
            {
                return sources.FirstOrDefault(val => val.IsDefaultValue == true);
            }
            else return new AttributeSource()
            {
                EnumId = AttributeDefinition,
                Value = "",
                LocalizationKey = ""
            };
        }

        public void Init(Dictionary<AttributeDefinitionEnum, object> additionalInfo)
        {
            AdditionalInfo = additionalInfo;
        }
        public void SetAttributeDefinition(AttributeDefinitionEnum attributeDefinition)
        {
            AttributeDefinition = attributeDefinition;
            _attributesInfo = DomainExtensions.GetEnumAttributes<AttributeDefinitionEnum, AttributeInfoAttribute>(AttributeDefinition).SingleOrDefault() ?? null;
        }
        public object GetNameToExportFromValue(BaseInfoItem<long, string> value)
        {
            if (_attributesInfo != null && _attributesInfo.TypeName != null)
            { 
                Type t = Type.GetType(_attributesInfo.TypeName);
                var typeConverter = TypeDescriptor.GetConverter(t);
                // Recupero l'EnumSerializationName per l'enumerativo considerato (se esiste)
                object enumerative;
                if (value.Value==null || value.Value == string.Empty)
                {

                    // Trovo l'enumerativo corrisponde a EnumValue
                    enumerative = typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture, Convert.ToInt32(value.Id));
                }
                else //caso in cui sia stato convertito l'enumerativo-numero in stringa
                {
                    enumerative = GetEnumFromStringValue(value.Value);
                }
                // Converto l'enumerativo in stringa, come EnumSerializationName
                var serializationName = typeConverter.ConvertTo(null, CultureInfo.InvariantCulture, enumerative, typeof(string));
                if (serializationName != null)
                    return serializationName.ToString(); // Esiste un SerializationName corrispondente al valore che gli è stato passato
                else
                {
                    // Non esiste un SerializationName corrispondente al valore che gli è stato passato (tool creati senza valori di Default)
                    // Esporto con il valore di default di quell'enumerativo
                    DefaultValueAttribute[] attributes = (DefaultValueAttribute[])t.GetCustomAttributes(typeof(DefaultValueAttribute), false);
                    if (attributes != null && attributes.Length > 0)
                        return typeConverter.ConvertTo(null, CultureInfo.InvariantCulture, attributes[0].Value, typeof(string)).ToString();
                    else
                        return null;
                }

            }
            else
                return null;
        }
        public string GetEnumValueFromSerializationName(string serializationName)
        {
            if (_attributesInfo != null && _attributesInfo.TypeName != null)
            {
                var res = DomainExtensions.GetIntEnumValueFromString(_attributesInfo.TypeName, serializationName);
                if (res.Success)
                    return JsonConvert.SerializeObject(new BaseInfoItem<long, string>() {Id=res.Value, Value=string.Empty });
            }
            return null;
        }
        public ValueTypeEnum GetValueType()
        {
            return _attributesInfo.ValueType;
        }

        public object GetEnumFromStringValue(string enumInString)
        {
            var typeConverter = TypeDescriptor.GetConverter(Type.GetType(_attributesInfo.TypeName));
            return typeConverter.ConvertFrom(null, CultureInfo.InvariantCulture, enumInString);
           
        }

        public object GetEnumValueFromAttributeValue(BaseInfoItem<long, string> value)
        {
            var type = Type.GetType(_attributesInfo.TypeName);
            return Enum.Parse(Type.GetType(_attributesInfo.TypeName), value.Id.ToString());
        }

   
    }
}
