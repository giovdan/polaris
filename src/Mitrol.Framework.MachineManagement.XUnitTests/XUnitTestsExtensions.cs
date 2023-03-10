namespace XUnitTests
{
    using Mitrol.Framework.MachineManagement.Application.Models;

    public static class XUnitTestsExtensions
    {
        public static EntityItem ToEntity(this EntityListItem entityListItem)
        {
            return new EntityItem
            {
                Id = entityListItem.Id,
                DisplayName = entityListItem.DisplayName,
                EntityType = entityListItem.EntityType
            };
        }
    }
}
