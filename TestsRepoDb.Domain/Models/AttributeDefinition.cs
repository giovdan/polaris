namespace RepoDbVsEF.Domain.Models
{
    using RepoDbVsEF.Domain.Enums;
    using RepoDbVsEF.Domain.Interfaces;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AttributeDefinition")]
    public class AttributeDefinition: BaseEntity, IHasRowVersion
    {
        public AttributeDefinitionEnum EnumId { get; set; }
        public string DisplayName { get; set; }
        public EntityTypeEnum EntityTypeId { get; set; }
      [ConcurrencyCheck, DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public long RowVersion { get; set; }
    }
}
