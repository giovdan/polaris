namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using System;
    using System.Linq.Expressions;

    public interface IQuantityBackLogRepository: IRepository<QuantityBackLog, IMachineManagentDatabaseContext>
    {
        Result Remove(Expression<Func<QuantityBackLog, bool>> predicate);
    }
}
