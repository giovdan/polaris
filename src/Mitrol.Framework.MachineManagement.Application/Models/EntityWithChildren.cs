namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using HotChocolate;
    using System.Collections.Generic;
    using System.Linq;

    public class EntityWithChildren: EntityItem
    {
        [GraphQLName("Children")]
        public IEnumerable<EntityItem> Children { get; set; }

        public EntityWithChildren()
        {
            Children = Enumerable.Empty<EntityItem>();
        }

        public EntityWithChildren(EntityItem entity, IEnumerable<EntityItem> children)
        {
            Id = entity.Id;
            Attributes = entity.Attributes;
            EntityType = entity.EntityType;
            DisplayName = entity.DisplayName;
            Children = children;
        }
    }
}
