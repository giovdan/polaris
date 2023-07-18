namespace Mitrol.Framework.MachineManagement.Domain.Views
{
    using Mitrol.Framework.Domain.Enums;
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class EntityAttribute
    {
        public long Id { get; set; }
        public long EntityId { get; set; }
        public EntityTypeEnum EntityTypeId { get; set; }
        public string LocalizationKey { get; set; }
        public decimal? Value { get; set; }
        public string TextValue { get; set; }
        public string DisplayName { get; set; }
        public long AttributeDefinitionLinkId { get; set; }
        public AttributeDefinitionEnum EnumId { get; set; }
        public AttributeDataFormatEnum DataFormat { get; set; }
        [Column(TypeName = "ENUM ('Geometric', 'Process', 'Identifier', 'Generic')")]
        public AttributeTypeEnum AttributeType { get; set; }
        [Column(TypeName = "ENUM ('Number','String','Enum','Bool','Date')")]
        public AttributeKindEnum AttributeKind { get; set; }
        [Column(TypeName = "ENUM('Edit','Label','Combo','ListBox','Check','Override','Image','MultiValue')")]
        public ClientControlTypeEnum ControlType { get; set; }
        [Column(TypeName = "ENUM('Critical','High','Medium','Normal','ReadOnly')")]
        public ProtectionLevelEnum ProtectionLevel { get; set; }
        [Column(TypeName = "ENUM('Default','PlasmaHPR','PlasmaXPR')")]
        public ProcessingTechnologyEnum ProcessingTechnology { get; set; }
        [Column(TypeName = "ENUM('Optional','Fundamental','Preview')")]
        public AttributeScopeEnum AttributeScopeId { get; set; }
        [Column(TypeName = "ENUM ('None','DeltaValue','DeltaPercentage')")]
        public OverrideTypeEnum OverrideType { get; set; }
        public int Priority { get; set; }
        public bool IsCodeGenerator { get; set; }
        public AttributeDefinitionGroupEnum AttributeDefinitionGroupId { get; set; }
        public string HelpImage { get; set; }
        public int GroupPriority { get; set; }
        [Column(TypeName = "ENUM ('Profile','Tool','ToolRange', 'ToolHolder','ToolSubRangeBevel','ToolSubRangeTrueHole','ToolRangeMarking','ProgramCode','ProgramItem','ProgramPiece','Piece','StockItem','OperationType','ProductionRow','Material','OperationAdditionalItem')")]
        public ParentTypeEnum ParentType { get; set; }
    }

    public static class EntityAttributeExtensions
    {
        public static string FormattedValue(this EntityAttribute attribute)
        {
            string value = string.Empty;

            if (attribute.AttributeKind == AttributeKindEnum.String)
                return attribute.TextValue;
            else
            {
                
                //se il numero troncato è uguale al numero convertito significa che non ha decimali e quindi va presentato senza decimali
                if (Math.Truncate(attribute.Value.GetValueOrDefault()) == attribute.Value.GetValueOrDefault())
                    value = attribute.Value.GetValueOrDefault().ToString();
                else
                    //altrimenti verrà presentato con due decimali.
                    value = attribute.Value.GetValueOrDefault().ToString("F2");
            }

            return value;
        }
    }
}
