namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IEntityRepository : IRepository<Entity, IMachineManagentDatabaseContext>
    {
        Entity RawUpdate(Entity entity);
        Entity Get(string displayName);

        #region < Tools/ToolRanges specs >
        IEnumerable<PlasmaToolMaster> FindPlasmaToolMasters(Expression<Func<PlasmaToolMaster, bool>> predicate);
        #endregion
    }
}
