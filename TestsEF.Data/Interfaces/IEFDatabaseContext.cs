namespace RepoDbVsEF.EF.Data.Interfaces
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.Domain.Models;
    
    public interface IEFDatabaseContext: IDatabaseContext
    {
        int SaveChanges();
        EntityEntry Entry(object entity);
        DatabaseFacade Database { get; }
        DbSet<Entity> Entities { get; set; }
        DbSet<AttributeDefinition> AttributeDefinitions { get; set; }
        DbSet<AttributeValue> AttributeValues { get; set; }
        DbSet<ChildLink> ChildLinks { get; set; }
    }
}
