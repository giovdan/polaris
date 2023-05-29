namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("RTMBackLog")]
    public class RTMBackLog: AuditableEntity
    {
        public RTMBackLog() : base()
        {
        }

        public RMTypeEnum RecordType { get; set; }
        public long PieceId { get; set; }
        public long ProgramId { get; set; }
        public int PieceNumber { get; set; }
        public int ProgramNumber { get; set; }

        public long RTM1 { get; set; }
        public long RTM2 { get; set; }
        public long RTM3 { get; set; }
        public long RTM4 { get; set; }
        public long RTM5 { get; set; }
        public long RTM6 { get; set; }
        public long RTM7 { get; set; }
        public long RTM8 { get; set; }
        public long RTM9 { get; set; }
        public long RTM10 { get; set; }
        public long RTM11 { get; set; }
        public long RTM12 { get; set; }
        public long RTM13 { get; set; }
        public long RTM14 { get; set; }
        public long RTM15 { get; set; }
        public long RTM16 { get; set; }
        public long RTM17 { get; set; }
        public long RTM18 { get; set; }
        public long RTM19 { get; set; }
        public long RTM20 { get; set; }
        public long RTM21 { get; set; }
        public long RTM22 { get; set; }
        public long RTM23 { get; set; }
        public long RTM24 { get; set; }
        public long RTM25 { get; set; }
        public long RTM26 { get; set; }
        public long RTM27 { get; set; }
        public long RTM28 { get; set; }
        public long RTM29 { get; set; }
        public long RTM30 { get; set; }
        public long RTM31 { get; set; }
        public long RTM32 { get; set; }
        public long RTM33 { get; set; }
        public long RTM34 { get; set; }
        public long RTM35 { get; set; }
        public long RTM36 { get; set; }
        public long RTM37 { get; set; }
        public long RTM38 { get; set; }
        public long RTM39 { get; set; }
        public long RTM40 { get; set; }
        public long RTM41 { get; set; }
        public long RTM42 { get; set; }
        public long RTM43 { get; set; }
        public long RTM44 { get; set; }
        public long RTM45 { get; set; }
        public long RTM46 { get; set; }
        public long RTM47 { get; set; }
        public long RTM48 { get; set; }
        public long RTM49 { get; set; }
        public long RTM50 { get; set; }
        public long RTM51 { get; set; }
        public long RTM52 { get; set; }
        public long RTM53 { get; set; }
        public long RTM54 { get; set; }
        public long RTM55 { get; set; }
        public long RTM56 { get; set; }
        public long RTM57 { get; set; }
        public long RTM58 { get; set; }
        public long RTM59 { get; set; }
        public long RTM60 { get; set; }
        public long RTM61 { get; set; }
        public long RTM62 { get; set; }
        public long RTM63 { get; set; }
        public long RTM64 { get; set; }
        public long BackLogStartDate { get; set; }
        public long BackLogEndDate { get; set; }
        
    }
}
