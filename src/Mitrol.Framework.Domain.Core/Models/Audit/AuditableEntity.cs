using Mitrol.Framework.Domain.Core.Interfaces;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mitrol.Framework.Domain.Core.Models
{
    public abstract class AuditableEntity : BaseEntity, IAuditableEntity
    {
        [MaxLength(32)]
        [DefaultValue("MITROL")]
        [Required]
        public string CreatedBy { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }

        [MaxLength(32)]
        [DefaultValue("MITROL")]
        public string UpdatedBy { get; set; }

        public DateTime? UpdatedOn { get; set; }

        [Obsolete()]
        public string TimeZoneId { get; set; }

        [NotMapped]
        public bool PreserveUpdatedOn { get; set; }
    }

    public abstract class AuditableExtendedEntity: AuditableEntity
    {
        public int NumberOfUpdates { get; set; }
    }
}