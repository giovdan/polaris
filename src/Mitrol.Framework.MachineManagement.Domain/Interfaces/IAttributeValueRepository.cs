namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Views;

    public interface IAttributeValueRepository: IRepository<AttributeValue, IMachineManagentDatabaseContext>
    {
        IEnumerable<EntityStatusAttribute> GetEntityStatusAttributes(Expression<Func<EntityStatusAttribute, bool>> predicate);
        AttributeOverrideValue GetOverrideValue(long attributeValueId);
        IEnumerable<AttributeOverrideValue> FindAttributeOverridesBy(Expression<Func<AttributeOverrideValue, bool>> predicate);
        void BatchUpdate(IEnumerable<AttributeValue> attributeValues);
        void BatchUpdateOverrides(IEnumerable<AttributeOverrideValue> attributeOverrideValues);
        int BatchInsertOverrides(IEnumerable<AttributeOverrideValue> attributeOverrideValues);
        int Remove(Expression<Func<AttributeValue, bool>> predicate);
        #region < Bulk operations >
        int BulkUpdate(IEnumerable<AttributeValue> attributeValues);
        int BulkInsertOverrides(IEnumerable<AttributeOverrideValue> attributeOverrideValues);
        #endregion
    }
}
