namespace RepoDbVsEF.Application.Models
{
    using HotChocolate;
    using RepoDbVsEF.Domain.Enums;

    public class EntityListItem
    {
        [GraphQLName("Id")]
        public long Id { get; set; }
        [GraphQLName("DisplayName")]
        public string DisplayName { get; set; }
        [GraphQLName("EntityType")]
        public EntityTypeEnum EntityType { get; set; }
    }
}
