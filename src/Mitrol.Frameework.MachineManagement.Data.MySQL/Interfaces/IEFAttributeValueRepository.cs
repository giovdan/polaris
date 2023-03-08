namespace Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces
{
    using System.Collections.Generic;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;

    public interface IEFAttributeValueRepository: IRepository<AttributeValue, IEFDatabaseContext>
    {
        int BulkUpdate(IEnumerable<AttributeValue> attributeValues);
        void BatchUpdate(IEnumerable<AttributeValue> attributeValues);
    }
}
