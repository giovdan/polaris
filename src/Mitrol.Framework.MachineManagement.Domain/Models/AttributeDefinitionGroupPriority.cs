namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AttributeDefinitionGroupPriority")]
    public class AttributeDefinitionGroupPriority : AuditableEntity
    {
        [Required()]
        public AttributeDefinitionGroupEnum AttributeDefinitionGroupId { get; set; }
        [Required()]
        public EntityTypeEnum EntityType { get; set; }
        [Required()]
        [DefaultValue(999)]
        public int Priority { get; set; }
    }
}