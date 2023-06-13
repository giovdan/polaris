namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.Domain.Core.Interfaces;

    public interface IAttributeDefinitionRepository : IReadOnlyRepository<AttributeDefinition, IMachineManagentDatabaseContext>
    {
        
    }
}
