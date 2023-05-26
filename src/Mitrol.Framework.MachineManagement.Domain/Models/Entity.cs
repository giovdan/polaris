namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Core.Models.Database;
    using Mitrol.Framework.Domain.Enums;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Entity")]
    public class Entity : BaseEntityWithRowVersion, IAuditableEntity
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public EntityTypeEnum EntityTypeId { get; set; }
        public long? SecondaryKey { get; set; }
        public long? MasterIdentifierId { get; set; }
        public EntityStatusEnum Status { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        [NotMapped]
        public bool PreserveUpdatedOn { get; set; }
        public string TimeZoneId { get; set; }
        public Entity()
        {
            Code = Guid.NewGuid().ToString();
            DisplayName = $"{EntityTypeId.ToString().ToUpper()}_{Code}";
        }

        public virtual MasterIdentifier MasterIdentifier { get; set; }
    }

}
