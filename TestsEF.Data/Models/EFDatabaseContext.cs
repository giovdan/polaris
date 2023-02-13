namespace RepoDbVsEF.EF.Data.Models
{
    using Microsoft.EntityFrameworkCore;
    using RepoDbVsEF.Domain.Models;
    using RepoDbVsEF.Domain.Interfaces;
    using RepoDbVsEF.EF.Data.Core;
    using RepoDbVsEF.EF.Data.Interfaces;

    public class EFDatabaseContext: BaseDbContext, IEFDatabaseContext
    {
        public EFDatabaseContext(DbContextOptions options) : base(options)
        {

        }

        #region Definitions
        public DbSet<Entity> Entities { get; set; }
        public DbSet<AttributeDefinition> AttributeDefinitions { get; set; }
        public DbSet<AttributeValue> AttributeValues { get; set; }
        public DbSet<ChildLink> ChildLinks { get; set; }

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
