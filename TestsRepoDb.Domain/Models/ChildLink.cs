namespace Mitrol.Framework.Domain.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("ChildLink")]
    public class ChildLink: BaseEntity
    {
        public long ParentId { get; set; }
        public long ChildId { get; set; }
        public int RowNumber { get; set; }
        public int Level { get; set; }

        public virtual MasterEntity Parent { get; set; }
        public virtual MasterEntity Child { get; set; }
    }
}
