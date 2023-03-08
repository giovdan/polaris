using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;

namespace Mitrol.Framework.Domain.Core.Models
{
    public partial class TrackAuditEntry
    {
        public TrackAuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public string loggedUser { get; set; } ///logged User
        public EntityState EntityState { get; set; }
        public EntityEntry Entry { get; }
        public string UserSessionId { get; set; }
        public string TableName { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
    }
}