namespace RepoDbVsEF.Application.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public class EntityWithChildren: Entity
    {
        [JsonProperty("Children")]
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
