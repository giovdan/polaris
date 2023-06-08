
namespace Mitrol.Framework.Domain.Core.Models.Database
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using System;

    public class BaseEntityWithRowVersion : BaseEntity, IHasRowVersion
    {
        public string RowVersion { get; set; }
    }

    public class BaseAuditableEntityWithRowVersion : BaseEntityWithRowVersion, IAuditableEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        //public bool PreserveUpdatedOn { get; set; }
        public string TimeZoneId { get; set; }
    }
}
