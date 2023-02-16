namespace XUnitTests.Models
{
    using RepoDbVsEF.Domain.Enums;
    using System.Collections.Generic;
    using System.Linq;

    public class EntityToCreate
    {
        public string DisplayName { get; set; }
        public EntityTypeEnum EntityType { get; set; }
        public IEnumerable<AttributeItem> Attributes {get;set;}

        public EntityToCreate()
        {
            Attributes = Enumerable.Empty<AttributeItem>();
        }
    }
}
