namespace Mitrol.Framework.MachineManagement.Application.RulesHandlers
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Attributes;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;

    using System.Collections.Generic;
    using System.Linq;

    public class StockItemRulesHandler : BaseEntityRulesHandler, IEntityRulesHandler
    {
        private IMachineConfigurationService MachineConfigurationService
                   => ServiceFactory.GetService<IMachineConfigurationService>();

        public StockItemRulesHandler(IServiceFactory serviceFactory) : base(serviceFactory)
        {
        }

        private IEnumerable<AttributeSource> HandleAll(IEnumerable<AttributeSource> attributeSources)
        {
            //Se sto ragionando sull'attributo profileType
            if (attributeSources.All(s => s.EnumId == AttributeDefinitionEnum.ProfileType))
            {
                //Recupero i profili "gestiti"
                var managedProfiles = MachineConfigurationService.ConfigurationRoot.Machine.Profiles
                    .Where(profileConfiguration => profileConfiguration.Value)
                    .Select(profileConfiguration => profileConfiguration.Key.ToString()).ToHashSet();

                attributeSources = attributeSources.Where(a => managedProfiles.Contains(a.Code));
            }

            return attributeSources;
        }

        
        public IEnumerable<T> HandleAll<T>(IEnumerable<T> attributes)
        {
            switch (typeof(T))
            {
                case
                    var type when type == typeof(AttributeSource):
                    {
                        var attributeSource = (attributes).Cast<AttributeSource>();
                        return HandleAll(attributeSource).Cast<T>();
                    }
            }
            return attributes;
        }

        public void Init(Dictionary<AttributeDefinitionEnum, object> additionalInfo)
        {
            additionalInfo = new Dictionary<AttributeDefinitionEnum, object>();
        }
    }
}
