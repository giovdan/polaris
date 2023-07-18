namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("QuantityBackLog")]
    public class QuantityBackLog: AuditableEntity
    {
        public long EntityId { get; set; }

        public int TotalQuantity { get; set; }

        /// <summary>
        /// Quantità eseguite
        /// Qf
        /// </summary>
        public int ExecutedQuantity { get; set; }

        /// <summary>
        /// Quantità da caricare(<= TotalQuantity)
        /// </summary>
        public int QuantityTobeLoaded { get; set; }

        /// <summary>
        /// Quantità caricate(<= QuantityTobeLoaded)
        /// </summary>
        public int QuantityLoaded { get; set; }

        public virtual Entity Entity { get; set; }
    }
}
