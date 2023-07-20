namespace Mitrol.Framework.MachineManagement.Domain.Interfaces
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Models.Views;
    using Mitrol.Framework.MachineManagement.Domain.Views;

    public interface IMachineManagentDatabaseContext: IDatabaseContext
    {
        int SaveChanges();
        EntityEntry Entry(object entity);
        void SetEntity<TEntity>(TEntity entity, EntityState entityState) where TEntity : BaseEntity;
        
        DbSet<Entity> Entities { get; set; }
        DbSet<EntityType> EntityTypes { get; set; }
        DbSet<AttributeDefinition> AttributeDefinitions { get; set; }
        DbSet<AttributeValue> AttributeValues { get; set; }
        DbSet<EntityLink> EntityLinks { get; set; }
        DbSet<AttributeDefinitionLink> AttributeDefinitionLinks { get; set; }
        DbSet<DetailIdentifier> DetailIdentifiers { get; set; }
        DbSet<DetailIdentifierMaster> DetailIdentifierMasters { get; set; }
        DbSet<AttributeOverrideValue> AttributeOverrideValues { get; set; }
        DbSet<MachineParameter> MachineParameters { get; set; }
        DbSet<MachineParameterLink> MachineParameterLinks { get; set; }
        DbSet<PlasmaToolMaster> PlasmaToolMasters { get; set; }
        DbSet<Tool> Tools { get; set; }
        DbSet<EntityStatusAttribute> EntityStatusAttributes { get; set; }
        DbSet<EntityWithInfo> EntitiesWithInfo { get; set; }
        DbSet<QuantityBackLog> QuantityBackLogs { get; set; }
        DbSet<EntityAttribute> EntityAttributes { get; set; }
    }
}
