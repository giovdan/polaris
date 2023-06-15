namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ToolStatusAttribute
    {
        // from AttributeValue
        public long Id { get; set; }
        public long EntityId { get; set; }
        public AttributeDataFormatEnum DataFormatId { get; set; }
        public int Priority { get; set; }
        public decimal Value { get; set; }
        public string TextValue { get; set; }
        public long AttributeDefinitionLinkId { get; set; }
        public AttributeDefinitionEnum EnumId { get; set; }
        public string DisplayName { get; set; }
        [Column(TypeName = "ENUM ('Geometric', 'Process', 'Identifier', 'Generic')")]
        public AttributeTypeEnum AttributeType { get; set; }
        [Column(TypeName = "ENUM('Edit','Label','Combo','ListBox','Check','Override','Image','MultiValue')")]
        public ClientControlTypeEnum ControlType { get; set; }
        [Column(TypeName = "ENUM ('Number','String','Enum','Bool','Date')")]
        public AttributeKindEnum AttributeKind { get; set; }
        [Column(TypeName = "ENUM('Critical','High','Medium','Normal','ReadOnly')")]
        public ProtectionLevelEnum ProtectionLevel { get; set; }
        public AttributeDefinitionGroupEnum GroupId { get; set; }
        public PlantUnitEnum PlantUnitId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is ToolStatusAttribute attribute &&
                   Id == attribute.Id && EntityId == attribute.EntityId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, EntityId);
        }
    }
}
