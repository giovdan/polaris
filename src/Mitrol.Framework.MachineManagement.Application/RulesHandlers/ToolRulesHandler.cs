namespace Mitrol.Framework.MachineManagement.Application.RulesHandlers
{
    using AutoMapper;
    using Mitrol.Framework.Domain.Configuration;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using System.Linq;

    public class ToolRulesHandler : IEntityRulesHandler<ToolDetailItem>
    {
        private IEntityHandlerFactory _handlerFactory;
        private IServiceFactory _serviceFactory;
        private IMapper _mapper;

        private IMachineConfigurationService MachineConfigurationService 
                    => _serviceFactory.GetService<IMachineConfigurationService>();

        public ToolRulesHandler(IEntityHandlerFactory handlerFactory, IServiceFactory serviceFactory)
        {
            _handlerFactory = handlerFactory;
            _serviceFactory = serviceFactory;
            _mapper = serviceFactory.GetService<IMapper>();
        }

        public ToolDetailItem Handle(ToolDetailItem model)
        {
            var machineType = MachineConfigurationService.ConfigurationRoot.Machine.Type;

            // Sforzo punta
            var attributeTorqueFeedRate = model.Attributes
                           .SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.TorqueFeedRate);
            if (attributeTorqueFeedRate != null)
            {
                // Visibile solo se CNC Mitrol
                attributeTorqueFeedRate.Hidden = machineType is MachineTypeEnum.TANG_GEMINI or MachineTypeEnum.TANG_TIPOG or MachineTypeEnum.TANG_EX5;
            }

            // Ciclo aspirazione trucioli
            var attributeChipExtractionEnable = model.Attributes
                           .SingleOrDefault(a => a.EnumId == AttributeDefinitionEnum.ChipExtractionEnable);
            if (attributeChipExtractionEnable != null)
            {
                if (machineType == MachineTypeEnum.TANG_GEMINI || machineType == MachineTypeEnum.TANG_TIPOG)
                {
                    var G250_2 = MachineConfigurationService.ConfigurationRoot.Cnc.Fanuc.GFunctions[GFunctionEnum.G250_2];
                    var G250_3 = MachineConfigurationService.ConfigurationRoot.Cnc.Fanuc.GFunctions[GFunctionEnum.G250_3];
                    // Solo se Gemini/TipoG ed abilitata aspirazione trucioli
                    attributeChipExtractionEnable.Hidden = !(G250_2 || G250_3);
                }
            }

            // Unità segatrice => Gitea #425
            if (model.ToolType is ToolTypeEnum.TS55 or ToolTypeEnum.TS56 or ToolTypeEnum.TS57
                && MachineConfigurationService.ConfigurationRoot.Setup.Saw.AnyUnit)
            {
                // Recupera la segatrice
                var saw = MachineConfigurationService.ConfigurationRoot.Setup.Saw.Units.SingleOrDefault(s => s.Unit == UnitEnum.S);

                if (saw != null)
                {
                    model.Attributes = model.Attributes.Select(a =>
                    {
                        switch (saw.BladeForwardSpeedType)
                        {
                            case BladeForwardSpeedTypeEnum.Linear:
                                {
                                    a.Hidden = a.EnumId is AttributeDefinitionEnum.SectionBladeForwardSpeed or
                                                           AttributeDefinitionEnum.TeethBladeForwardSpeed;
                                }
                                break;
                            case BladeForwardSpeedTypeEnum.Section:
                                {
                                    a.Hidden = a.EnumId is AttributeDefinitionEnum.LinearBladeForwardSpeed or
                                                           AttributeDefinitionEnum.TeethBladeForwardSpeed;
                                }
                                break;
                            case BladeForwardSpeedTypeEnum.Teeth:
                                {
                                    a.Hidden = a.EnumId is AttributeDefinitionEnum.LinearBladeForwardSpeed or
                                                           AttributeDefinitionEnum.SectionBladeForwardSpeed;
                                }
                                break;
                            default:
                                {
                                    a.Hidden = a.EnumId is AttributeDefinitionEnum.SectionBladeForwardSpeed or
                                                           AttributeDefinitionEnum.TeethBladeForwardSpeed;
                                }
                                break;
                        }
                        return a;
                    }).ToList();
                }
            }

            return model;
        }
    }
}
