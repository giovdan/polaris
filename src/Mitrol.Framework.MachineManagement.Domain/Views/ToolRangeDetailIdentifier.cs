using Mitrol.Framework.Domain.Enums;
using Mitrol.Framework.MachineManagement.Domain.Enums;
using System;

namespace Mitrol.Framework.MachineManagement.Domain.Models.Views
{
    public class ToolRangeDetailIdentifier
    {
        public long Id { get; set; }
        public long MasterId { get; set; }
        public AttributeDefinitionEnum EnumId { get; set; }
        public AttributeDataFormatEnum DataFormatId { get; set; }
        public AttributeKindEnum AttributeKindId { get; set; }
        public string DisplayName { get; set; }
        public string Value { get; set; }
        public int Priority { get; set; }
        public long RowId { get; set; }
        public long? RowParentId { get; set; }
        public long ToolMasterId { get; set; }
        public long? RowParentMasterId { get; set; }
        public ParentTypeEnum ParentTypeId { get; set; }
        public string TypeName { get; set; }
        public bool IsMainFilter { get; set; }
        public long ToolTypeId { get; set; }
        public int MinRequiredConsole { get; set; }
    }

    public static class ToolRangeDetailIdentifierExtensions
    {
        public static string GetValue(this ToolRangeDetailIdentifier detailIdentifier
                                , MeasurementSystemEnum conversionSystem)
        {
            return string.Empty;
            //return new AttributeValue
            //{
            //    AttributeDefinition = new AttributeDefinition
            //    {
            //        EnumId = detailIdentifier.EnumId,
            //        AttributeKindId = detailIdentifier.AttributeKindId,
            //        DataFormatId = detailIdentifier.DataFormatId,
            //        TypeName = detailIdentifier.TypeName
            //    },
            //    ParentTypeId = detailIdentifier.ParentTypeId,
            //    TextValue = detailIdentifier.AttributeKindId == AttributeKindEnum.String ? detailIdentifier.Value : string.Empty,
            //    Value = detailIdentifier.AttributeKindId == AttributeKindEnum.String ? 0: Convert.ToDecimal(detailIdentifier.Value)
            //}.GetAttributeValue(conversionSystem).ToString();
        }
    }


}
