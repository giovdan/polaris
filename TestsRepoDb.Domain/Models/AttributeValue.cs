namespace RepoDbVsEF.Domain.Models
{
    using RepoDbVsEF.Domain.Interfaces;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AttributeValue")]
    public class AttributeValue: BaseEntityWithRowVersion, IAuditableEntity
    {
        public ulong AttributeDefinitionId { get; set; }
        public ulong EntityId { get; set; }
        public decimal? Value { get; set; }
        public string TextValue { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual AttributeDefinition AttributeDefinition { get; set; }
        public virtual DatabaseEntity Entity { get; set; }
    }
}
