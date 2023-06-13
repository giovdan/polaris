namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface IDetailIdentifierRepository: IReadOnlyRepository<DetailIdentifier, IMachineManagentDatabaseContext>
    {
        IEnumerable<DetailIdentifierMaster> GetIdentifiers(Expression<Func<DetailIdentifierMaster, bool>> predicate);
    }
}
