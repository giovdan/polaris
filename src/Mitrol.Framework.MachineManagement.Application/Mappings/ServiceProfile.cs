namespace Mitrol.Framework.MachineManagement.Application.Mappings
{
    using Mitrol.Framework.MachineManagement.Application.Models;
    using System.Linq;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using Mitrol.Framework.Domain;

    public class ServiceProfile: AutoMapper.Profile
    {
        public ServiceProfile()
        {
            CreateMap<Entity, EntityItem>()
                .ForMember(dest => dest.EntityType, opt => opt.MapFrom(s => s.EntityTypeId));

            CreateMap<EntityItem, Entity>()
                .ForMember(dest => dest.EntityTypeId, opt => opt.MapFrom(s => s.EntityType));

            CreateMap<Entity, EntityListItem>()
                .ForMember(dest => dest.EntityType, opt => opt.MapFrom(s => s.EntityTypeId));

            CreateMap<AttributeValue, AttributeItem>()
                .ForPath(dest => dest.Value.CurrentValue, opt => opt.MapFrom(s => s.Value))
                .ForPath(dest => dest.Value.CurrentValueId, opt => opt.MapFrom(s => s.Value))
                .ForMember(dest => dest.EnumId, opt => opt.MapFrom(s => s.AttributeDefinitionLink.AttributeDefinition.EnumId));

            CreateMap<AttributeItem, AttributeValue>()
                .ForMember(dest => dest.Value, opt => opt.Ignore())
                .ForMember(dest => dest.TextValue, opt => opt.Ignore());

            CreateMap<AttributeDefinition, AttributeItem>()
                .ForMember(dest => dest.AttributeDefinitionId, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.EnumId, opt => opt.MapFrom(s => s.EnumId));

            CreateMap<IGrouping<Entity, AttributeValue>, EntityItem>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.Key.Id))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(s => s.Key.DisplayName))
                .ForMember(dest => dest.EntityType, opt => opt.MapFrom(s => s.Key.EntityTypeId))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(s => s.AsEnumerable()));

            CreateMap<Entity, ToolListItem>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(s => s.DisplayName))
                .ForMember(dest => dest.CodeGenerators, opt => opt.Ignore())
                .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.SecondaryKey))
                .ForMember(dest => dest.InnerId, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.ToolType, opt => opt.MapFrom(s => s.EntityTypeId.ToToolType()))
                .ForMember(dest => dest.PlantUnit, opt => opt.MapFrom(s => s.EntityTypeId.GetPlantUnit()))
                .ForMember(dest => dest.InnerId, opt => opt.MapFrom(s => s.SecondaryKey));

            CreateMap<Entity,ToolDetailItem>()
                .ForMember(dest => dest.InnerId, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.SecondaryKey))
                .ForMember(dest => dest.ToolType, opt => opt.MapFrom(s => s.EntityTypeId.ToToolType()))
                .ForMember(dest => dest.ImageCode, opt => opt.MapFrom(s => s.EntityTypeId.DisplayName()))
                .ForMember(dest => dest.Identifiers, opt => opt.Ignore())
                .ForMember(dest => dest.Attributes, opt => opt.Ignore())
                .ForMember(dest => dest.PlantUnit, opt => opt.MapFrom(s => s.EntityTypeId.GetPlantUnit()))
                .ForMember(dest => dest.UnitTypeLocalizationKey, opt => opt.Ignore())
                .ForMember(dest => dest.StatusLocalizationKey, opt => opt.Ignore());

            CreateMap<DetailIdentifierMaster, IdentifierDetailItem>()
                .ForMember(dest => dest.LocalizationKey, opt => opt.MapFrom(s => $"{MachineManagementExtensions.LABEL_IDENTIFIER}_{s.DisplayName.ToUpper()}"))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(s => s.Priority))
                .ForMember(dest => dest.ItemDataFormat, opt => opt.MapFrom(s => s.DataFormat));

            CreateMap<DetailIdentifierMaster, CodeGeneratorItem>()
                .ForMember(dest => dest.Order, opt => opt.MapFrom(s => s.Priority))
                .ForMember(dest => dest.UMLocalizationKey, opt => opt.MapFrom(s => $"{DomainExtensions.GENERIC_LABEL}_{s.DataFormat.ToString().ToUpper()}"))
                .ForMember(dest => dest.ItemDataFormat, opt => opt.MapFrom(s => s.DataFormat))
                .ForMember(dest => dest.InnerValue, opt => opt.MapFrom(s => s.Value));


        }
    }
}
