namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("AttributeOverrideValue")]
    public class AttributeOverrideValue : AuditableEntity
    {
        [Required()]
        /// <summary>
        /// Collegamento AttributeValue
        /// </summary>
        public long AttributeValueId { get; set; }
        [Required()]
        public OverrideTypeEnum OverrideType { get; set; }
        [Required()]
        public decimal Value { get; set; }

        public virtual AttributeValue AttributeValue { get; set; }
    }
}
