namespace RepoDbVsEF.Application.Mappings
{
    using RepoDbVsEF.Application.Models;
    using RepoDbVsEF.Domain.Models;

    public class ServiceProfile: AutoMapper.Profile
    {
        public ServiceProfile()
        {
            CreateMap<DatabaseEntity, Entity>()
                .ForMember(dest => dest.EntityType, opt => opt.MapFrom(s => s.EntityTypeId));

            CreateMap<Entity, DatabaseEntity>()
                .ForMember(dest => dest.EntityTypeId, opt => opt.MapFrom(s => s.EntityType));

            CreateMap<AttributeValue, AttributeItem>();

            CreateMap<AttributeItem, AttributeValue>();
        }
    }
}
