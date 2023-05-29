namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RCMBackLog")]
    public class RCMBackLog: AuditableEntity
    {
        public RMTypeEnum RecordType { get; set; }
        public long PieceId { get; set; }
        public long ProgramId { get; set; }
        public int PieceNumber { get; set; }
        public int ProgramNumber { get; set; }
        public long BackLogStartDate { get; set; }
        public long BackLogEndDate { get; set; }

        public decimal RCM1 { get; set; }
        public decimal RCM2 { get; set; }
        public decimal RCM3 { get; set; }
    }
}
