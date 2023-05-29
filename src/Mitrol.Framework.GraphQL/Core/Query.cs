namespace Mitrol.Framework.GraphQL.Core
{
    using HotChocolate.Data;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using System.Collections.Generic;
    using System.Linq;

    public class Query
    {
        [UseSorting]
        public IQueryable<ToolListItem> GetTools(IToolService toolService)
        {
            toolService.SetSession(NullUserSession.Instance);
            return toolService.GetAll().AsQueryable();
        }

        public ToolDetailItem GetTool(IToolService toolService, long id)
        {
            toolService.SetSession(NullUserSession.Instance);
            var result = toolService.Get(id);
            return result.Success ? result.Value : null;
        }

        //public IEnumerable<AttributeItem> GetAttributeDefinitionByType(
        //        IEntityService entityService, EntityTypeEnum type)
        //{
        //    entityService.SetSession(NullUserSession.Instance);
        //    return entityService.GetAttributesByType(type);
        //}
    }
}
