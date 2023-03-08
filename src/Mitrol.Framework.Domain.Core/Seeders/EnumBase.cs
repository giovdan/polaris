namespace Mitrol.Framework.Domain.Core.Seeders
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    public class EnumBase<TEnum> where TEnum : struct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual long Id { get; set; }

        [Required]
        [MaxLength(32)]
        public virtual string Code { get; set; }

        [Required()]
        [MaxLength(255)]
        public virtual string Description { get; set; }

        [Required()]
        [MaxLength(50)]
        public virtual string DisplayName { get; set; }
    }
}