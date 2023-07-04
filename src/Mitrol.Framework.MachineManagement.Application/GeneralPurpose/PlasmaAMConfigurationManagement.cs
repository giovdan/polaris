namespace Mitrol.Framework.MachineManagement.Application.GeneralPurpose
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
    using Mitrol.Framework.MachineManagement.Application.Services;
    using Mitrol.Framework.Domain.Attributes;

    public class PlasmaAMConfigurationManagement : MachineManagementBaseService, IAttributeDefinitionEnumManagement
    {
        public AttributeDefinitionEnum AttributeDefinition { get; set; }
        private IEntityRepository ToolRepository => ServiceFactory.GetService<IEntityRepository>();

        public PlasmaAMConfigurationManagement(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }

       // TODO
        public IEnumerable<AttributeSource> FindAttributeSourceValues()
        {
            //// Recupero i toolmasterId di tool di Plasma che hanno tabelle associate
            //var plasmaToolMasterIds = ToolRepository.FindPlasmaToolMasters(id => id.ToolTypeId == ToolTypeEnum.TS51)
            //            .Select(id => id.ToolMasterId).Distinct()
            //            .ToHashSet();

            //// Recupero la lista dei valori dell'identificatore PlasmaCurrent per tutti i toolMasterIds recuperati
            //// precedentemente
            //var plasmaIdentifiers = AttributeValueRepository.FindIdentifiers(id => plasmaToolMasterIds.Contains(id.MasterIdentifierId)
            //                && id.ParentTypeId == ParentTypeEnum.Tool
            //                && id.AttributeDefinition.EnumId == AttributeDefinitionEnum.PlasmaCurrent)
            //                .ToHashSet();

            //return Mapper.Map<IEnumerable<AttributeSource>>(plasmaIdentifiers)
            //    .DistinctBy(a => a.Value);
            return null;
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
