namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Core.Models.Database;
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AttributeDefinition")]
    public class AttributeDefinition : BaseEntityWithRowVersion
    {
        public AttributeDefinitionEnum EnumId { get; set; }
        public string DisplayName { get; set; }
        public string HelpImage { get; set; }
        [Required()]
        public AttributeTypeEnum AttributeType { get; set; }
        [Required()]
        public AttributeDataFormatEnum DataFormat { get; set; }
        [DefaultValue(ClientControlTypeEnum.Edit)]
        public ClientControlTypeEnum ControlType { get; set; }
        [DefaultValue(AttributeKindEnum.Number)]
        public AttributeKindEnum AttributeKind { get; set; }
        [DefaultValue(OverrideTypeEnum.None)]
        public OverrideTypeEnum OverrideTypeId { get; set; }
        public string TypeName { get; set; }
        [DefaultValue(GroupEnum.USERS)]
        public GroupEnum Owner { get; set; }
        [DefaultValue(AttributeDefinitionGroupEnum.Generic)]
        public AttributeDefinitionGroupEnum GroupId { get; set; }
        [DefaultValue(0)]
        public int Priority { get; set; }
    }

    [Table("AttributeDefinitionLink")]
    public class AttributeDefinitionLink: BaseEntity
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
