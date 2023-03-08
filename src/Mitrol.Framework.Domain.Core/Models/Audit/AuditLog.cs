using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mitrol.Framework.Domain.Core.Models
{
    [Table("AuditLog")]
    public class AuditLog : BaseEntity
    {
        [Required()]
        [MaxLength(100)]
        public string TableName { get; set; }

        public string SessionId { get; set; }

        [Required()]
        public string EntityState { get; set; }

        [Required()]
        [MaxLength(32)]
        public string ChangedBy { get; set; }

        [Required()]
        public DateTime ChangedOn { get; set; }

        [Required()]
        public string KeyValue { get; set; }

        public string OldValues { get; set; }
        public string NewValues { get; set; }

        public string TimeZoneId { get; set; }
    }
}