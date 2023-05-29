namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ToolStatusAttribute
    {
        // from AttributeValue
        public long Id { get; set; }
        public long EntityId { get; set; }
        public AttributeDataFormatEnum DataFormatId { get; set; }
        public decimal Value { get; set; }
        public string TextValue { get; set; }
        public long AttributeDefinitionId { get; set; }
        public AttributeDefinitionEnum EnumId { get; set; }
        public string DisplayName { get; set; }
        public AttributeTypeEnum AttributeTypeId { get; set; }
        public ClientControlTypeEnum ControlTypeId { get; set; }
        public AttributeKindEnum AttributeKindId { get; set; }
        [Column(TypeName = "ENUM('Critical','High','Medium','Normal','ReadOnly')")]
        public ProtectionLevelEnum ProtectionLevel { get; set; }
        public AttributeDefinitionGroupEnum GroupId { get; set; }
        public int Priority { get; set; }

        // from ToolType
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
