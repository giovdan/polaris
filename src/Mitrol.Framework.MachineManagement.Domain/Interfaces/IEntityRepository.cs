namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Models.Views;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IEntityRepository : IRepository<Entity, IMachineManagentDatabaseContext>
    {
        Entity RawUpdate(Entity entity);
        Entity Get(string displayName);
        Entity GetBySecondaryKey(long secondaryKey, EntityTypeEnum entityType);
        #region < Tools Management >
        IEnumerable<PlasmaToolMaster> FindPlasmaToolMasters(Expression<Func<PlasmaToolMaster, bool>> predicate);
        #endregion
    }
}
