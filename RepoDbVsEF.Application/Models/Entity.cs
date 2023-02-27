namespace RepoDbVsEF.Application.Models
{
    using HotChocolate;
    using System.Collections.Generic;
    using System.Linq;

    public class Entity: EntityListItem
    {
        [GraphQLName("Attributes")]
        public IEnumerable<AttributeItem> Attributes { get; set; }

        public Entity()
        {
            Attributes = Enumerable.Empty<AttributeItem>();
        }
    }
}
