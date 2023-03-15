namespace Mitrol.Framework.MachineManagement.Data.RepDb.Interfaces
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;


    public interface IRepoDbAttributeDefinitionRepository: IRepository<AttributeDefinition, IRepoDbDatabaseContext>
    {
    }
}
