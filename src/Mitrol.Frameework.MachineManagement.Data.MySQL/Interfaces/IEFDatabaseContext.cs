namespace Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;

    public interface IEFDatabaseContext: IDatabaseContext
    {
        int SaveChanges();
        EntityEntry Entry(object entity);
        DatabaseFacade Database { get; }
        DbSet<Entity> Entities { get; set; }
        DbSet<AttributeDefinition> AttributeDefinitions { get; set; }
        DbSet<AttributeValue> AttributeValues { get; set; }
        DbSet<EntityLink> EntityLinks { get; set; }
        DbSet<AttributeDefinitionLink> AttributeDefinitionLinks { get; set; }
    }
}
