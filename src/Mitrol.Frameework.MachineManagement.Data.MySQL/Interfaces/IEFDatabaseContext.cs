namespace Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    
    public interface IEFDatabaseContext: IDatabaseContext
    {
        int SaveChanges();
        EntityEntry Entry(object entity);
        DatabaseFacade Database { get; }
        DbSet<MasterEntity> Entities { get; set; }
        DbSet<AttributeDefinition> AttributeDefinitions { get; set; }
        DbSet<AttributeValue> AttributeValues { get; set; }
        DbSet<ChildLink> ChildLinks { get; set; }
    }
}
