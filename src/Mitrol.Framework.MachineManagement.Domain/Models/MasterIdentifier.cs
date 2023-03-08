namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    //Tabella usata per gestire gli identificatori univoci per Tool e ToolRange
    //Filtri (ToolTypeId, ParentTypeId)
    [Table("MasterIdentifier")]
    public class MasterIdentifier : AuditableEntity
    {
        public MasterIdentifier()
        {

        }

        [Required()]
        public string HashCode { get; set; }
        [Required()]
        public long EntityId { get; set; }
        [Required()]

        public virtual ICollection<DetailIdentifier> Details { get; set; }
        public virtual Entity Entity { get; set; }
    }
}
