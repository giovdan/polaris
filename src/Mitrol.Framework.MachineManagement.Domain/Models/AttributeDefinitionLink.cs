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
        public string HelpImage { get; set; }
        [DefaultValue(ClientControlTypeEnum.Edit)]
        [Column(TypeName = "ENUM('Edit','Label','Combo','ListBox','Check','Override','Image','MultiValue')")]
        public ClientControlTypeEnum ControlType { get; set; }
        public bool IsCodeGenerator { get; set; }
        public bool IsSubFilter { get; set; }
        public bool IsStatusAttribute { get; set; }
        [Column(TypeName = "ENUM('Optional','Fundamental','Preview')")]
        public AttributeScopeEnum AttributeScopeId { get; set; }
        [Column(TypeName = "ENUM('Critical','High','Medium','Normal','ReadOnly')")]
        public ProtectionLevelEnum ProtectionLevel { get; set; }
        [Column(TypeName = "ENUM('DataDefault','LastInserted')")]
        public AttributeBehaviorEnum DefaultBehavior { get; set; }
        public decimal? LastInsertedValue { get; set; }
        public string LastInsertedTextValue { get; set; }
        [DefaultValue(AttributeDefinitionGroupEnum.Generic)]
        public AttributeDefinitionGroupEnum GroupId { get; set; }
        [DefaultValue(0)]
        public int Priority { get; set; }

        public virtual AttributeDefinition AttributeDefinition { get; set; }
    }
}
