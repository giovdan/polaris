namespace XUnitTests
{
    using Mitrol.Framework.MachineManagement.Application.Models;

    public static class XUnitTestsExtensions
    {
        public static Entity ToEntity(this EntityListItem entityListItem)
        {
            return new Entity
            {
                Id = entityListItem.Id,
                DisplayName = entityListItem.DisplayName,
                EntityType = entityListItem.EntityType
            };
        }
    }
}
