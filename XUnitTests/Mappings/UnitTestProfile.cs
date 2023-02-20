namespace XUnitTests.Mappings
{
    using RepoDbVsEF.Application.Models;
    using RepoDbVsEF.Domain.Models;
    using XUnitTests.Models;

    public class UnitTestProfile: AutoMapper.Profile
    {

        public UnitTestProfile()
        {
            CreateMap<DatabaseEntity, Entity>()
                .ForMember(dest => dest.EntityType, opt => opt.MapFrom(s => s.EntityTypeId));

            CreateMap<Entity, DatabaseEntity>()
                .ForMember(dest => dest.EntityTypeId, opt => opt.MapFrom(s => s.EntityType));

            CreateMap<EntityToCreate, DatabaseEntity>()
                .ForMember(dest => dest.EntityTypeId, opt => opt.MapFrom(s => s.EntityType));

            CreateMap<AttributeItem, AttributeValue>()
                .ForMember(dest => dest.TextValue, opt => opt.Ignore())
                .ForMember(dest => dest.Value, opt => opt.Ignore());

        }
    }
}
