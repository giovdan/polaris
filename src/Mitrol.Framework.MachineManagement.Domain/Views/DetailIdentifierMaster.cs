namespace Mitrol.Framework.MachineManagement.Domain.Views
{
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Entity identifiers View 
    /// </summary>
    public class DetailIdentifierMaster
    {
        public long Id { get; set; }
        public string HashCode { get; set; }
        public string Value { get; set; }
        public string DisplayName { get; set; }
        public EntityTypeEnum EntityTypeId { get; set; }
        public long AttributeDefinitionLinkId { get; set; }
        public int Priority { get; set; }
        [Column(TypeName = "ENUM('Optional','Fundamental','Preview')")]
        public AttributeScopeEnum AttributeScopeId { get; set; }
        [Column(TypeName = "ENUM('Number','String','Enum','Bool','Date')")]
        public AttributeKindEnum AttributeKind { get; set; }
        public AttributeDataFormatEnum DataFormat { get; set; }
        public bool IsCodeGenerator { get; set; }
        public string TypeName { get; set; }
        public AttributeDefinitionGroupEnum GroupId { get; set; }
        public AttributeDefinitionEnum EnumId { get; set; }
        [Column(TypeName = "ENUM ('Geometric', 'Process', 'Identifier', 'Generic')")]
        public AttributeTypeEnum AttributeType { get; set; }
        [DefaultValue(ClientControlTypeEnum.Edit)]
        [Column(TypeName = "ENUM('Edit','Label','Combo','ListBox','Check','Override','Image','MultiValue')")]
        public ClientControlTypeEnum ControlType { get; set; }
    }
}
