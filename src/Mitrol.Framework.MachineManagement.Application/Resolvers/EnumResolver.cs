namespace Mitrol.Framework.MachineManagement.Application.Resolvers
{
    using Mitrol.Framework.Domain;
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Attributes;
    using Mitrol.Framework.MachineManagement.Application.GeneralPurpose;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Services;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using System.Linq;

    public class EnumResolver : IResolver<IAttributeDefinitionEnumManagement, AttributeDefinitionEnum>
    {
        private readonly IServiceFactory _serviceFactory;

        public EnumResolver(IServiceFactory serviceFactory)
        {
            _serviceFactory = serviceFactory;
        }

        public IAttributeDefinitionEnumManagement Resolve(AttributeDefinitionEnum serviceKind)
        {
            var attributesInfo = DomainExtensions.GetEnumAttributes<AttributeDefinitionEnum, AttributeInfoAttribute>(serviceKind);
            IAttributeDefinitionEnumManagement management = null;
            switch (attributesInfo.First().ValueType)
             {
                case (ValueTypeEnum.EnumFromFile):
                    management = _serviceFactory.GetService<ExternalFileConfigurationManagement>();
                    break;       
                case (ValueTypeEnum.StaticEnum):
                    management= _serviceFactory.GetService<FlatEnumConfigurationManagement>();
                    break;
                case (ValueTypeEnum.DynamicEnum):
                case ValueTypeEnum.MultiValue:
                    management = _serviceFactory.GetService<DynamicEnumConfigurationManagement>();
                    break;
                case ValueTypeEnum.PlasmaAM:
                    management = _serviceFactory.GetService<PlasmaAMConfigurationManagement>();
                    break;

            }
            management?.SetAttributeDefinition(serviceKind);
            return management;
        }
    }
}
