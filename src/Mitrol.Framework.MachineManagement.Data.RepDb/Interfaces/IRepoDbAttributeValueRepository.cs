namespace Mitrol.Framework.MachineManagement.Data.RepDb.Interfaces
{
    using System.Collections.Generic;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;

    public interface IRepoDbAttributeValueRepository: IRepository<AttributeValue, IRepoDbDatabaseContext>
    {
        int BulkUpdate(IEnumerable<AttributeValue> attributeValues);
        int BatchUpdate(IEnumerable<AttributeValue> attributeValues);
    }
}
