namespace RepoDbVsEF.RepoDb.Data.Interfaces
{
    using RepoDbVsEF.Domain.Models;
    using RepoDbVsEF.Domain.Interfaces;
    using System.Collections.Generic;
    using RepoDbVsEF.Data.Interfaces;

    public interface IRepoDbAttributeValueRepository: IRepository<AttributeValue, IRepoDbDatabaseContext>
    {
        int BulkUpdate(IEnumerable<AttributeValue> attributeValues);
        int BatchUpdate(IEnumerable<AttributeValue> attributeValues);
    }
}
