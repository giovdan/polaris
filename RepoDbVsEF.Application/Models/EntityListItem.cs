namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using HotChocolate;
    using Mitrol.Framework.Domain.Enums;

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
