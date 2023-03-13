namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("AttributeDefinitionLink")]
    public class AttributeDefinitionLink : BaseEntity
    {
        public long AttributeDefinitionId { get; set; }
        public EntityTypeEnum EntityTypeId { get; set; }
        [Required()]
        public AttributeDataFormatEnum DataFormat { get; set; }
        [DefaultValue(ClientControlTypeEnum.Edit)]
        public ClientControlTypeEnum ControlType { get; set; }
        [DefaultValue(AttributeKindEnum.Number)]
        public AttributeKindEnum AttributeKind { get; set; }
        [DefaultValue(AttributeDefinitionGroupEnum.Generic)]
        public AttributeDefinitionGroupEnum GroupId { get; set; }
        [DefaultValue(0)]
        public int Priority { get; set; }


        public virtual AttributeDefinition AttributeDefinition { get; set; }
    }
}
