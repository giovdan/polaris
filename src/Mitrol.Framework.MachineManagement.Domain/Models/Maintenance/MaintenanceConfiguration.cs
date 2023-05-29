namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("MaintenanceConfiguration")]
    public class MaintenanceConfiguration: AuditableEntity
    {
        [Required()]
        public long MaintenanceId { get; set; }
        /// <summary>
        /// Tempo di fine delle ripetizioni
        /// Se Type = Calendar      => TIMESTAMP
        /// Se Type = WorkingHours  => MINUTI
        /// Se -1 => per sempre
        /// </summary>
        public long UntilTime { get; set; }
        [Required()]
        /// <summary>
        /// Prossima scadenza
        /// Se Type = Calendar      => TIMESTAMP
        /// Se Type = WorkingHours  => MINUTI
        /// </summary>
        public long ExpirationTime { get; set; }
        public bool IsActive { get; set; }
        [Required()]
        /// <summary>
        /// Cadenza ripetizioni
        /// Se Type = Calendar      => valore dell'enumerato MaintenanceActionRepeatEachEnum 
        /// Set Type = WorkingHours => Valore libero (> 0)
        /// </summary>
        public int RepeatEach { get; set; }
        [Required()]
        /// <summary>
        /// Cadenza ripetizioni
        /// Se Type = Calendar      => MaintenanceNoticeModeEnum
        /// Set Type = WorkingHours => Valore libero (> 0)
        /// </summary>
        public int GivenNoticeMode { get; set; }
        /// <summary>
        /// Working hours correnti in minuti
        /// </summary>
        public long CurrentWorkingTime { get; set; }

        public virtual Maintenance Maintenance { get; set; }
    }
}
