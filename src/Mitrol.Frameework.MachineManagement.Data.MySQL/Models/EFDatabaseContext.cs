namespace Mitrol.Framework.MachineManagement.Data.MySQL.Models
{
    using Microsoft.EntityFrameworkCore;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Data.MySQL.Interfaces;
    using Mitrol.Framework.MachineManagement.Domain.Models;

    public class EFDatabaseContext: BaseDbContext, IEFDatabaseContext
    {
        public EFDatabaseContext(DbContextOptions options) : base(options)
        {

        }

        #region Definitions
        public DbSet<Entity> Entities { get; set; }
        public DbSet<AttributeDefinition> AttributeDefinitions { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<EntityLink> EntityLinks { get; set; }
        public DbSet<AttributeDefinitionLink> AttributeDefinitionLinks { get; set; }

        public void SetSession(IUserSession session)
        {
            base.SetSession(session);
        }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entity>()
                .HasIndex(u => u.Code)
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

            modelBuilder.Entity<AttributeDefinition>()
                .Property(c => c.RowVersion)
                .IsRowVersion()
                .ValueGeneratedOnAddOrUpdate();

            modelBuilder.Entity<AttributeValue>()
                .Property(c => c.RowVersion)
                .IsRowVersion()
                .ValueGeneratedOnAddOrUpdate();
        }

    }
}
