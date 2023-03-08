namespace Mitrol.Framework.MachineManagement.Data.MySQL.Models
{
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
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public bool PreserveUpdatedOn { get; set; }

        public Entity()
        {
            Code = Guid.NewGuid().ToString();
            DisplayName = $"{EntityTypeId.ToString().ToUpper()}_{Code}";
        }
    }

}
