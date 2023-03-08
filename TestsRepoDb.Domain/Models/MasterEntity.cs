namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MasterEntity")]
    public class MasterEntity : BaseEntityWithRowVersion, IAuditableEntity
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public EntityTypeEnum EntityTypeId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }


        public MasterEntity()
        {
            Code = Guid.NewGuid().ToString();
            DisplayName = $"{EntityTypeId.ToString().ToUpper()}_{Code}";
        }
    }
}
