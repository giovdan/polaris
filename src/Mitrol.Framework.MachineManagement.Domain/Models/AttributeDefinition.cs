namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AttributeDefinition")]
    public class AttributeDefinition : BaseEntity
    {
        public AttributeDefinitionEnum EnumId { get; set; }
        public string DisplayName { get; set; }
        [Required()]
        [Column(TypeName = "ENUM ('Geometric', 'Process', 'Identifier', 'Generic')")]
        public AttributeTypeEnum AttributeType { get; set; }
        [Required()]
        public AttributeDataFormatEnum DataFormat { get; set; }
        [DefaultValue(AttributeKindEnum.Number)]
        [Column(TypeName = "ENUM ('Number','String','Enum','Bool','Date')")]
        public AttributeKindEnum AttributeKind { get; set; }
        public string TypeName { get; set; }
        [DefaultValue(OverrideTypeEnum.None)]
        [Column(TypeName = "ENUM ('None','DeltaValue','DeltaPercentage")]
        public OverrideTypeEnum OverrideType { get; set; }
    }
}