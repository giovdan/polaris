using Microsoft.EntityFrameworkCore;
using Mitrol.Framework.Domain.Core.Attributes;
using Mitrol.Framework.Domain.Core.Extensions;
using Mitrol.Framework.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mitrol.Framework.Domain.Core.Models
{
    public abstract class BaseAuditableDbContext : BaseDbContext, IDatabaseContext
    {
        protected List<TrackAuditEntry> _entitiesToAudit;

        protected AuditLog ToAuditLog(TrackAuditEntry e)
        {
            var audit = new AuditLog();
            audit.TableName = e.TableName;
            audit.ChangedOn = DateTime.UtcNow;
            audit.ChangedBy = e.loggedUser;
            audit.SessionId = e.UserSessionId;
            audit.EntityState = e.EntityState.ToString();
            audit.KeyValue = JsonConvert.SerializeObject(e.KeyValues);
            audit.OldValues = e.OldValues.Count == 0 ? null : JsonConvert.SerializeObject(e.OldValues);
            audit.NewValues = e.NewValues.Count == 0 ? null : JsonConvert.SerializeObject(e.NewValues);
            audit.TimeZoneId = TimeZoneInfo.Local.DisplayName;
            return audit;
        }

        public DbSet<AuditLog> AuditLogs { get; set; }

        public BaseAuditableDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// Get All entity changes for tracking them in audit table
        /// </summary>
        /// <returns></returns>
        protected List<TrackAuditEntry> GetAuditChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<TrackAuditEntry>();

            var auditablEntries = ChangeTracker.Entries()
                    .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified || x.State == EntityState.Deleted);

            string loggedUser = GetUserNameToLog();
            foreach (var entry in auditablEntries)
            {
                //Audit table entry is not auditable
                if (entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                //Check if Audit is disabled
                if (!entry.Entity.GetType().IsDefined(typeof(DisableAuditing), true))
                {
                    var auditEntry = new TrackAuditEntry(entry);
                    auditEntry.TableName = entry.Metadata.GetTableName();
                    auditEntry.EntityState = entry.State;
                    auditEntry.loggedUser = loggedUser;
                    auditEntry.UserSessionId = UserSession?.SessionId;
                    auditEntries.Add(auditEntry);
                    foreach (var property in entry.Properties)
                    {
                        // Check for Shadow Property
                        if (property.Metadata.IsShadowProperty())
                            continue;

                        //Check primary Key
                        if (property.Metadata.IsPrimaryKey())
                        {
                            auditEntry.KeyValues[property.Metadata.Name] = property.CurrentValue;
                            continue;
                        }

                        switch (entry.State)
                        {
                            case EntityState.Added:
                                auditEntry.NewValues[property.Metadata.Name] = property.CurrentValue;
                                break;

                            case EntityState.Deleted:
                                auditEntry.OldValues[property.Metadata.Name] = property.OriginalValue;
                                break;

                            case EntityState.Modified:
                                auditEntry.OldValues[property.Metadata.Name] = property.OriginalValue;
                                auditEntry.NewValues[property.Metadata.Name] = property.CurrentValue;
                                break;
                        }
                    }
                }
            }
            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries;
        }

        /// <summary>
        /// Abstract function for Track Audit Logic
        /// </summary>
        protected virtual void ApplyTrackAuditLogics()
        {
            if (_entitiesToAudit.Any())
            {
                _entitiesToAudit.ForEach(e =>
                {
                    //Set primary Key new value for Entry in Added State
                    if (e.EntityState == EntityState.Added)
                    {
                        var keys = e.Entry.Properties.Where(x => x.Metadata.IsPrimaryKey());
                        //e.KeyValues[key.Metadata.Name] = e.Entry.CurrentValues[key.Metadata.Name];
                        keys.ForEach(k => e.KeyValues[k.Metadata.Name] = e.Entry.CurrentValues[k.Metadata.Name]);
                    }
                    AuditLogs.Add(ToAuditLog(e));
                });
            }
        }
    }
}