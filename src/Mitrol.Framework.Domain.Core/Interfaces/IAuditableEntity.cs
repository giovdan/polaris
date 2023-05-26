using System;

namespace Mitrol.Framework.Domain.Core.Interfaces
{
    public interface IAuditableEntity
    {
        string CreatedBy { get; set; }
        DateTime CreatedOn { get; set; }
        string UpdatedBy { get; set; }
        DateTime? UpdatedOn { get; set; }
        bool PreserveUpdatedOn { get; set; }
        string TimeZoneId { get; set; }
    }
}