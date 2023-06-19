namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Views;

    public interface IMachineManagentDatabaseContext: IDatabaseContext
    {
        int SaveChanges();
        EntityEntry Entry(object entity);
        DatabaseFacade Database { get; }
        DbSet<Entity> Entities { get; set; }
        DbSet<AttributeDefinition> AttributeDefinitions { get; set; }
        DbSet<AttributeValue> AttributeValues { get; set; }
        DbSet<EntityLink> EntityLinks { get; set; }
        DbSet<AttributeDefinitionLink> AttributeDefinitionLinks { get; set; }
        DbSet<DetailIdentifier> DetailIdentifiers { get; set; }
        DbSet<DetailIdentifierMaster> DetailIdentifierMasters { get; set; }
        DbSet<ToolStatusAttribute> ToolStatusAttributes { get; set; }
        DbSet<AttributeOverrideValue> AttributeOverrideValues { get; set; }
    }
}
