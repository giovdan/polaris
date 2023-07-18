namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Models.Views;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IEntityRepository : IRepository<Entity, IMachineManagentDatabaseContext>
    {
        Entity RawUpdate(Entity entity);
        Entity Get(string displayName);
        Entity GetBySecondaryKey(long secondaryKey, EntityTypeEnum entityType);
        Result BulkRemove(Expression<Func<EntityWithInfo,bool>> predicate);
        #region < Tools Management >
        IEnumerable<PlasmaToolMaster> FindPlasmaToolMasters(Expression<Func<PlasmaToolMaster, bool>> predicate);
        IEnumerable<Tool> FindTools(Expression<Func<Tool, bool>> predicate);
        IEnumerable<EntityStatusAttribute>  GetStatusAttributes(Func<EntityStatusAttribute, bool> predicate);
        #endregion
    }
}
