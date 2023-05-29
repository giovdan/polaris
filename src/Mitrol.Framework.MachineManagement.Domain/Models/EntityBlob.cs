namespace Mitrol.Framework.MachineManagement.Domain.Models.Production
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EntityBlob")]
    public class EntityBlob : AuditableEntity
    {
        [Required()]
        public long EntityId { get; set; }
        [Required()]
        public byte[] Content { get; set; }
        [Required()]
        public EntityTypeEnum EntityTypeId { get; set; }
        [Required()]
        [Column(TypeName = "ENUM('Graphics','IsoCode')")]
        public BlobTypeEnum BlobType { get; set; }
    }
}
