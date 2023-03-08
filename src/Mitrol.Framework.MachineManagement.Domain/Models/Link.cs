namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Models;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("Link")]
    public class Link: BaseEntity
    {
        public EntityRelationshipTypeEnum RelationTypeId { get; set; }
        public long RelatedEntityId { get; set; }
        public long EntityId { get; set; }
        public int RowNumber { get; set; }
        public int Level { get; set; }
    }
}
