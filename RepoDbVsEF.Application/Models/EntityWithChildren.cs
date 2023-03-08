namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using HotChocolate;
    using System.Collections.Generic;
    using System.Linq;

    public class EntityWithChildren: Entity
    {
        [GraphQLName("Children")]
        public IEnumerable<Entity> Children { get; set; }

        public EntityWithChildren()
        {
            Children = Enumerable.Empty<Entity>();
        }

        public EntityWithChildren(Entity entity, IEnumerable<Entity> children)
        {
            Id = entity.Id;
            Attributes = entity.Attributes;
            EntityType = entity.EntityType;
            DisplayName = entity.DisplayName;
            Children = children;
        }
    }
}
