namespace Mitrol.Framework.MachineManagement.Application.Services
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Attributes;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using System;
    using System.Collections.Generic;
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using System.Linq;
    using Newtonsoft.Json;
    using Mitrol.Framework.Domain.Core.Interfaces;

    public class PlasmaAMConfigurationManagement : MachineManagementBaseService, IAttributeDefinitionEnumManagement
    {
        public AttributeDefinitionEnum AttributeDefinition { get; set; }

        public PlasmaAMConfigurationManagement(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }

      
        public IEnumerable<AttributeSource> FindAttributeSourceValues()
        {
            var toolType51EntityTypes = new[]
            {
                EntityTypeEnum.ToolTS51, EntityTypeEnum.ToolTS51HPR, EntityTypeEnum.ToolTS51XPR
            };

            // Recupero i toolmasterId di tool di Plasma che hanno tabelle associate
            var hashCodes = EntityRepository.FindPlasmaToolMasters(id => toolType51EntityTypes(id.EntityTypeId))
                        .Select(id => id.HashCode).Distinct()
                        .ToHashSet();

            // Recupero la lista dei valori dell'identificatore PlasmaCurrent per tutti i toolMasterIds recuperati
            // precedentemente
            var plasmaIdentifiers = AttributeValueRepository.FindIdentifiers(id => hashCodes.Contains(id.HashCode)
                            && id.ParentTypeId == ParentTypeEnum.Tool
                            && id.AttributeDefinition.EnumId == AttributeDefinitionEnum.PlasmaCurrent)
                            .ToHashSet();

            return Mapper.Map<IEnumerable<AttributeSource>>(plasmaIdentifiers)
                .DistinctBy(a => a.Value);
        }

        public AttributeSource GetDefaultValue()
        {
            return null;
        }

        public object GetEnumFromStringValue(string enumInString)
        {
            return enumInString;
        }

        public object GetEnumValueFromAttributeValue(BaseInfoItem<long, string> value)
        {
            return value.Value;
        }

        public string GetEnumValueFromSerializationName(string serializationName)
        {
            return JsonConvert.SerializeObject(new BaseInfoItem<long, string>() { Id = 0, Value = serializationName }); ;
        }

        public object GetNameToExportFromValue(BaseInfoItem<long, string> value)
        {
            throw new NotImplementedException();
        }

        public ValueTypeEnum GetValueType()
        {
            return ValueTypeEnum.PlasmaAM;
        }

        public void Init(Dictionary<AttributeDefinitionEnum, object> additionalInfo)
        {
            throw new NotImplementedException();
        }

        public void SetAttributeDefinition(AttributeDefinitionEnum attributeDefinition)
        {
            
        }
    }
}
