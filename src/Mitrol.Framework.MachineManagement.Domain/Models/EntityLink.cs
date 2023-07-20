namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EntityLink")]
    public class EntityLink: BaseEntity
    {
        [Column(TypeName = "ENUM('Child','ForeignKey','Sibling')")]
        public EntityRelationshipTypeEnum RelationType { get; set; }
        public string RelatedEntityHashCode { get; set; }
        public string EntityHashCode { get; set; }
        public int RowNumber { get; set; }
        public int Level { get; set; }
    }
}
