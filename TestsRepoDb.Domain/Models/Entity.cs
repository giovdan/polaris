namespace RepoDbVsEF.Domain.Models
{
    using RepoDbVsEF.Domain.Enums;
    using RepoDbVsEF.Domain.Interfaces;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Entity")]
    public class Entity : BaseEntity, IHasRowVersion, IAuditableEntity
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
        public EntityTypeEnum EntityTypeId { get; set; }
        [ConcurrencyCheck, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long RowVersion { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }


        public Entity()
        {
            Code = Guid.NewGuid().ToString();
            DisplayName = $"{EntityTypeId.ToString().ToUpper()}_{Code}";
        }
    }
}
