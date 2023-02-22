namespace RepoDbVsEF.Application.Models
{
    using RepoDbVsEF.Domain.Enums;
    using System.Collections.Generic;
    using System.Linq;

    public class Entity
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public EntityTypeEnum EntityType { get; set; }
        public IEnumerable<AttributeItem> Attributes { get; set; }

        public Entity()
        {
            Attributes = Enumerable.Empty<AttributeItem>();
        }
    }
}
