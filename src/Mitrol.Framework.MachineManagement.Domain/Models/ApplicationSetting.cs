namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("ApplicationSetting")]
    public class ApplicationSetting: BaseEntity
    {
        [Required]
        [MaxLength(32)]
        public virtual string Code { get; set; }

        [Required()]
        [MaxLength(255)]
        public virtual string Description { get; set; }

        [Required()]
        [MaxLength(50)]
        public virtual string DisplayName { get; set; }

        [Required()]
        public string DefaultValue { get; set; }
    }
}
