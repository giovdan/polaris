namespace Mitrol.Framework.MachineManagement.Data.MySQL.Models
{
    using Microsoft.EntityFrameworkCore;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Extensions;
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;
    using Mitrol.Framework.MachineManagement.Domain.Models.Views;
    using Mitrol.Framework.MachineManagement.Domain.Views;
    using System.Linq;

    public class MachineManagementDatabaseContext: BaseDbContext, IMachineManagentDatabaseContext
    {
        public MachineManagementDatabaseContext(DbContextOptions options) : base(options)
        {

        }

        #region Definitions
        public DbSet<Entity> Entities { get; set; }
        public DbSet<AttributeDefinition> AttributeDefinitions { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<EntityLink> EntityLinks { get; set; }
        public DbSet<AttributeDefinitionLink> AttributeDefinitionLinks { get; set; }
        public DbSet<DetailIdentifier> DetailIdentifiers { get; set; }
        public DbSet<DetailIdentifierMaster> DetailIdentifierMasters { get; set; }
        public DbSet<AttributeOverrideValue> AttributeOverrideValues { get; set; }
        public DbSet<MachineParameter> MachineParameters { get; set; }
        public DbSet<MachineParameterLink> MachineParameterLinks { get; set; }
        public DbSet<PlasmaToolMaster> PlasmaToolMasters { get; set; }
        public DbSet<Tool> Tools { get; set; }
        public DbSet<EntityStatusAttribute> EntityStatusAttributes { get; set; }
        public DbSet<EntityWithInfo> EntitiesWithInfo { get; set; }
        public DbSet<QuantityBackLog> QuantityBackLogs { get; set; }
        public DbSet<EntityAttribute> EntityAttributes { get; set; }

        public void SetEntity<TEntity>(TEntity entity, EntityState entityState)
            where TEntity: BaseEntity
        {

            var local = Set<TEntity>().Local.SingleOrDefault(f => f.Id == entity.Id);
            if (local != null)
            {
                Entry(local).State = EntityState.Detached;
            }

            Entry(entity).State = entityState;
        }

        public new void SetSession(IUserSession session)
        {
            base.SetSession(session);
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity>()
                .HasIndex(u => u.DisplayName)
                .IsUnique();

            modelBuilder.Entity<Entity>().Property(t => t.Status)
                .HasDefaultValue(EntityStatusEnum.Available)
                .HasConversion<string>();

            modelBuilder.Entity<Entity>()
                .Property(c => c.RowVersion)
                .IsRowVersion()
                .ValueGeneratedOnAddOrUpdate(); 

            modelBuilder.Entity<AttributeDefinition>()
                .HasIndex(u => u.DisplayName)
                .IsUnique();

            modelBuilder.Entity<AttributeDefinition>().Property(a => a.AttributeKind)
                .HasDefaultValue(AttributeKindEnum.Number)
                .HasConversion<string>();

            modelBuilder.Entity<AttributeDefinition>().Property(a => a.AttributeType)
                .HasDefaultValue(AttributeTypeEnum.Generic)
                .HasConversion<string>();

            modelBuilder.Entity<AttributeDefinition>().Property(a => a.OverrideType)
                .HasDefaultValue(OverrideTypeEnum.None)
                .HasConversion<string>();

            modelBuilder.Entity<AttributeValue>()
                .Property(c => c.RowVersion)
                .IsRowVersion()
                .ValueGeneratedOnAddOrUpdate();

            #region Views
            modelBuilder.Entity<DetailIdentifierMaster>().ToView("DetailIdentifiersView").HasNoKey();
            modelBuilder.Entity<EntityStatusAttribute>().ToView("EntityStatusAttributesView").HasNoKey();
            modelBuilder.Entity<PlasmaToolMaster>().ToView("PlasmaToolMasters").HasNoKey();
            modelBuilder.Entity<Tool>().ToView("Tools").HasNoKey();
            modelBuilder.Entity<EntityWithInfo>().ToView("EntityListView").HasNoKey();
            modelBuilder.Entity<EntityAttribute>().ToView("EntityAttributesView").HasNoKey();
            #endregion
        }

    }
}
