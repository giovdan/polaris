namespace RepoDbVsEF.EF.Data.Interfaces
{
    using RepoDbVsEF.Domain.Models;
    using RepoDbVsEF.Domain.Interfaces;

    public interface IEFAttributeDefinitionRepository : IRepository<AttributeDefinition, IEFDatabaseContext>
    {
        
    }
}
