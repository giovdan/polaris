namespace Mitrol.Framework.MachineManagement.Application.Mappings
{
    using Mitrol.Framework.MachineManagement.Application.Models;
    using Mitrol.Framework.Domain.Models;
    using System.Linq;

    public class ServiceProfile: AutoMapper.Profile
    {
        public ServiceProfile()
        {
            CreateMap<MasterEntity, Entity>()
                .ForMember(dest => dest.EntityType, opt => opt.MapFrom(s => s.EntityTypeId));

            CreateMap<Entity, MasterEntity>()
                .ForMember(dest => dest.EntityTypeId, opt => opt.MapFrom(s => s.EntityType));

            CreateMap<MasterEntity, EntityListItem>()
                .ForMember(dest => dest.EntityType, opt => opt.MapFrom(s => s.EntityTypeId));

            CreateMap<AttributeValue, AttributeItem>()
                .ForPath(dest => dest.Value.CurrentValue, opt => opt.MapFrom(s => s.Value))
                .ForPath(dest => dest.Value.CurrentValueId, opt => opt.MapFrom(s => s.Value))
                .ForMember(dest => dest.EnumId, opt => opt.MapFrom(s => s.AttributeDefinition.EnumId));

            CreateMap<AttributeItem, AttributeValue>()
                .ForMember(dest => dest.Value, opt => opt.Ignore())
                .ForMember(dest => dest.TextValue, opt => opt.Ignore());

            CreateMap<AttributeDefinition, AttributeItem>()
                .ForMember(dest => dest.AttributeDefinitionId, opt => opt.MapFrom(s => s.Id))
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.EnumId, opt => opt.MapFrom(s => s.EnumId));

            CreateMap<IGrouping<MasterEntity, AttributeValue>, Entity>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(s => s.Key.Id))
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(s => s.Key.DisplayName))
                .ForMember(dest => dest.EntityType, opt => opt.MapFrom(s => s.Key.EntityTypeId))
                .ForMember(dest => dest.Attributes, opt => opt.MapFrom(s => s.AsEnumerable()));
        }
    }
}
