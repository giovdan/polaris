namespace Mitrol.Framework.MachineManagement.Application.GeneralPurpose
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Attributes;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Services;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ExternalFileConfigurationManagement : MachineManagementBaseService, IAttributeDefinitionEnumManagement
    {
        protected Dictionary<AttributeDefinitionEnum, object> AdditionalInfo { get; set; }
        protected IMachineConfigurationService MachineConfigurationService => ServiceFactory.GetService<IMachineConfigurationService>();
        public AttributeDefinitionEnum AttributeDefinition { get; set; }
        
        private AttributeInfoAttribute _attributesInfo;

        public ExternalFileConfigurationManagement(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }
       

        public IEnumerable<AttributeSource> FindAttributeSourceValues()
        {
            var sourceValues = new List<AttributeSource>();
            var attributesInfo = DomainExtensions.GetEnumAttributes<AttributeDefinitionEnum
                                , AttributeInfoAttribute>(AttributeDefinition);
          
            var values = MachineConfigurationService.GetSources(AttributeDefinition, AdditionalInfo);
            if (values != null)
                sourceValues = values.Select(font =>
                {
                    return new AttributeSource()
                    {
                        EnumId = AttributeDefinition,
                        Code = font.Code,
                        Value = font.Id.ToString(),
                        LocalizationKey = font.Code,
                        MustBeTranslated = false
                    };
                }).ToList();
            return sourceValues;
        }

        public AttributeSource GetDefaultValue()
        {
            //da implementare logica su file..
            return new AttributeSource()
            {
                EnumId = AttributeDefinition,
                Value = "0",
                LocalizationKey = ""
            };//occorrerebbe provvedere a dare il default nel caso configurazione da file.
           
        }

        public void Init(Dictionary<AttributeDefinitionEnum, object> additionalInfo)
        {
            AdditionalInfo = additionalInfo;
        }

        public void SetAttributeDefinition(AttributeDefinitionEnum attributeDefinition)
        {
            AttributeDefinition = attributeDefinition;
            _attributesInfo = DomainExtensions.GetEnumAttributes<AttributeDefinitionEnum
                            , AttributeInfoAttribute>(AttributeDefinition).SingleOrDefault() ?? null;
        }
        public object GetNameToExportFromValue(BaseInfoItem<long, string> value)
        {
            if (AttributeDefinition==AttributeDefinitionEnum.FontType)
            {
                return value.Id.ToString();
            }
            return null;
        }
        public string GetEnumValueFromSerializationName(string serializationName)
        {
            return JsonConvert.SerializeObject(new BaseInfoItem<long, string>() { Id = Convert.ToInt64(serializationName), Value = string.Empty });
        }
        public ValueTypeEnum GetValueType()
        {
            return _attributesInfo.ValueType;
        }
        public object GetEnumFromStringValue(string enumInString)
        {
            return null;
        }

        public object GetEnumValueFromAttributeValue(BaseInfoItem<long, string> value)
        {
            return value.Id;
        }
    }
}
