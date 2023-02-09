namespace RepoDbVsEF.Domain.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    [Table("ChildLink")]
    public class ChildLink: BaseEntity
    {
        public ulong ParentId { get; set; }
        public ulong ChildId { get; set; }
        public int RowNumber { get; set; }
        public int Level { get; set; }

        public virtual Entity Parent { get; set; }
        public virtual Entity Child { get; set; }
    }
}
