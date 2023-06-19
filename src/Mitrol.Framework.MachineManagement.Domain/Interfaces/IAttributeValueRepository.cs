namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;

    public interface IAttributeValueRepository: IRepository<AttributeValue, IMachineManagentDatabaseContext>
    {
        int BulkUpdate(IEnumerable<AttributeValue> attributeValues);
        void BatchUpdate(IEnumerable<AttributeValue> attributeValues);
        IEnumerable<ToolStatusAttribute> GetToolStatusAttributes(Expression<Func<ToolStatusAttribute, bool>> predicate);
        AttributeOverrideValue GetOverrideValue(long attributeValueId);
    }
}
