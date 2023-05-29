
namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MachineParameterLink")]
    public class MachineParameterLink: AuditableEntity
    {
        public int LinkId { get; set; }
        public long ParameterId { get; set; }
        public CncTypeEnum CncTypeId { get; set; } 
        public string Variable { get; set; }

        public virtual MachineParameter Parameter { get; set; }
    }
}
