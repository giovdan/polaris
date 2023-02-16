namespace XUnitTests.Mappings
{
    using RepoDbVsEF.Domain.Models;
    using XUnitTests.Models;

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
