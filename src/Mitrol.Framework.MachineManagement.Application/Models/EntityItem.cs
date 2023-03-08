namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using HotChocolate;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public class EntityItem: EntityListItem
    {
        [JsonProperty("Attributes")]
        [GraphQLName("Attributes")]
        public IEnumerable<AttributeItem> Attributes { get; set; }

        public EntityItem()
        {
            Attributes = Enumerable.Empty<AttributeItem>();
        }
    }
}
