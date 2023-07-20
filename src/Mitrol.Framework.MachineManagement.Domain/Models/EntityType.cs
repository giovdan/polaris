namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EntityType")]
    public class EntityType: BaseEntity
    {
        public string DisplayName { get; set; }
        public bool IsManaged { get; set; }
        [Column(TypeName = "ENUM('Profile','Tool','ToolRange','ToolHolder','ToolSubRangeBevel','ToolSubRangeTrueHole','ToolRangeMarking','ProgramCode','ProgramItem','ProgramPiece','Piece','StockItem','OperationType','ProductionRow','Material','HandlingElement','OperationAdditionalItem')")]
        public ParentTypeEnum ParentType { get; set; }
        public string InternalCode { get; set; }
        public long? SecondaryKey { get; set; }
    }
}
