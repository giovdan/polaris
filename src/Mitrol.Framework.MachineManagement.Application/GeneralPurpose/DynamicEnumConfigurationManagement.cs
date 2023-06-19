
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
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;
    public class DynamicEnumConfigurationManagement: MachineManagementBaseService
                    , IAttributeDefinitionEnumManagement
    {
        private AttributeInfoAttribute _attributesInfo;
        private IEntityRepository EntityRepository => ServiceFactory.GetService<IEntityRepository>();
        protected Dictionary<AttributeDefinitionEnum, object> AdditionalInfo { get; set; }
        public AttributeDefinitionEnum AttributeDefinition { get; set; }
       

        public DynamicEnumConfigurationManagement(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }
      

        public IEnumerable<AttributeSource> FindAttributeSourceValues()
        {
            var sourceValues = new List<AttributeSource>();
           
            
            var attr = new AttributeSource()
            {
                LocalizationKey = "",
                MustBeTranslated = false,
                Description = "",
                Code = "",
                Value = _attributesInfo.Url,
                EnumId = AttributeDefinition
            };
            sourceValues.Add(attr);

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
                Value = "0",
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
            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            EntityRepository.Attach(uow);
            var entity = EntityRepository.Get(value.Id);
            if (entity == null)
            {
                return value;
            }

            return new BaseInfoItem<long, string> { Id = entity.Id, Value = entity.DisplayName };
        }

        public string GetEnumValueFromSerializationName(string serializationName)
        {
            using var uow = UnitOfWorkFactory.GetOrCreate(UserSession);
            EntityRepository.Attach(uow);

            var entity = EntityRepository.Get(serializationName);

            if (entity != null)
            {
                return JsonConvert.SerializeObject(new BaseInfoItem<long, string>() { Id = entity.Id, Value = serializationName });
            }

            return null;
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
            return value.Value;
        }
    }
}
