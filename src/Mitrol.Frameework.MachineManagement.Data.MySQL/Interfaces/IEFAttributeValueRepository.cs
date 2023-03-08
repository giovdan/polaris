namespace Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces
{
    using System.Collections.Generic;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;

    public interface IEFAttributeValueRepository: IRepository<AttributeValue, IEFDatabaseContext>
    {
        int BulkUpdate(IEnumerable<AttributeValue> attributeValues);
        void BatchUpdate(IEnumerable<AttributeValue> attributeValues);
    }
}
