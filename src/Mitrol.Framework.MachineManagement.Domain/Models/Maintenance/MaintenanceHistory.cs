namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// Tabella storico interventi
    /// </summary>
    [Table("MaintenanceHistory")]
    public class MaintenanceHistory: AuditableEntity
    {
        [Required()]
        public long MaintenanceId { get; set; }
        /// <summary>
        /// Data intervento
        /// </summary>
        [Required()]
        public DateTime ExecutionDate { get; set; }
        /// <summary>
        /// Tipo di azione effettuata
        /// </summary>
        [Required()]
        public MaintenanceActionTypeEnum ActionType { get; set; }
        /// <summary>
        /// Tempo impiegato
        /// </summary>
        [Required()]
        public long TimeSpentOn { get; set; }
        [Required()]
        public string LoggedUser { get; set; }
        /// <summary>
        /// Note dell'operazione
        /// </summary>
        public string Notes { get; set; }

        public virtual Maintenance Maintenance { get; set; }
    }
}
