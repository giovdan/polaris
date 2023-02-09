namespace UnitTests.Mappings
{
    using RepoDbVsEF.Domain.Enums;
    using RepoDbVsEF.Domain.Models;
    using UnitTests.Models;

    public class UnitTestProfile: AutoMapper.Profile
    {

        public UnitTestProfile()
        {
            CreateMap<EntityToCreate, Entity>()
                .ForMember(dest => dest.EntityTypeId, opt => opt.MapFrom(s => s.EntityType));

            CreateMap<AttributeItem, AttributeValue>()
                .ForMember(dest => dest.TextValue, opt => opt.Ignore())
                .ForMember(dest => dest.Value, opt => opt.Ignore());

        }
    }
}
