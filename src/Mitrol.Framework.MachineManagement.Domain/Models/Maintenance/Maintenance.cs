namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Maintenance")]
    public class Maintenance: AuditableEntity
    {
        [Required()]
        public string Code { get; set; }
        [Required()]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required()]
        public MaintenanceCategoryEnum Category { get; set; }
        [Required()]
        public MaintenanceStatusEnum Status { get; set; }
        public bool IsMandatory { get; set; }
        public MaintenanceOwnerEnum Owner { get; set; }
        [Required()]
        public MaintenanceTypeEnum Type { get; set; }
        /// <summary>
        /// Tempo stimato in minuti
        /// </summary>
        public long EstimatedTime { get; set; }
    }
}
