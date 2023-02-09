namespace RepoDbVsEF.EF.Data.Interfaces
{
    using RepoDbVsEF.Domain.Models;
    using RepoDbVsEF.Domain.Interfaces;
    using System.Collections.Generic;

    public interface IEFAttributeValueRepository: IRepository<AttributeValue, IEFDatabaseContext>
    {
        int BulkUpdate(IEnumerable<AttributeValue> attributeValues);
        void BatchUpdate(IEnumerable<AttributeValue> attributeValues);
    }
}
