namespace Mitrol.Framework.MachineManagement.Data.MySQL.Core
{
    using Microsoft.EntityFrameworkCore;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    public class BaseDbContext : DbContext, IDatabaseContext
    {
        private const string DEFAULT_USERNAME = "MITROL";

        public IUserSession UserSession { get; set; }

        public BaseDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Get current logged User from Session
        /// </summary>
        /// <returns></returns>
        public virtual string GetUserNameToLog()
        {
            if (!string.IsNullOrEmpty(UserSession?.Username))
            {
                return UserSession.Username;
            }

            return DEFAULT_USERNAME;
        }

        private void ApplyAuditableLogic()
        {
            ChangeTracker.DetectChanges();
            var modifiedEntries = ChangeTracker.Entries()
                            .Where(x => x.Entity is IAuditableEntity
                            && (x.State == EntityState.Added || x.State == EntityState.Modified));

            string identityName = GetUserNameToLog();
            foreach (var entry in modifiedEntries)
            {
                //Check for AuditableEntity
                var entity = entry.Entity as IAuditableEntity;
                if (entity != null)
                {
                    DateTime now = DateTime.UtcNow;
                    if (entry.State == EntityState.Added)
                    {
                        entity.CreatedBy = identityName;
                        entity.CreatedOn = now;
                    }
                    else
                    {
                        base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                        base.Entry(entity).Property(x => x.CreatedOn).IsModified = false;
                    }

                    entity.UpdatedBy = identityName;
                    entity.UpdatedOn = now;
                }

                //Check for LoggedUser property
                var entityHasLoggedUser = entry.Entity as IHasLoggedUser;
                if (entityHasLoggedUser != null
                    && string.IsNullOrEmpty(entityHasLoggedUser.LoggedUser))
                {
                    entityHasLoggedUser.LoggedUser = identityName;
                }

                //Check if Has SessionId Property
                var entityHasSessionId = entry.Entity as IHasSessionId;
                if (entityHasSessionId != null
                    && string.IsNullOrEmpty(entityHasSessionId.SessionId))
                {
                    entityHasSessionId.SessionId = UserSession.SessionId;
                }

            }
        }

        /// <summary>
        /// Override DbContext.SaveChanges() for set default properties and audit entities modifications
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            var saved = false;
            int result = 0;
            while (! saved)
            {
                try
                {
                    ApplyAuditableLogic();
                    result = base.SaveChanges();
                    ChangeTracker.Clear();
                    saved = true;
                }
                catch (DbUpdateConcurrencyException dce)
                {
                    foreach(var entry in dce.Entries)
                    {
                        var proposedValues = entry.CurrentValues;
                        var databaseValues = entry.GetDatabaseValues();

                        foreach (var property in proposedValues.Properties)
                        {
                            var proposedValue = proposedValues[property];
                            var databaseValue = databaseValues[property];
                        }

                        entry.OriginalValues.SetValues(databaseValues);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;
        }

        /// <summary>
        /// Async SaveChanges()
        /// </summary>
        /// <returns></returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                return base.SaveChangesAsync(cancellationToken);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SetSession(IUserSession session)
        {
            this.UserSession = session;
        }
    }
}
