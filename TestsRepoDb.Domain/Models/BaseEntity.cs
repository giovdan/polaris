namespace Mitrol.Framework.Domain.Models
{
    using System.ComponentModel.DataAnnotations;

    public class BaseEntity
    {
        [Key()]
        public long Id { get; set; }
    }
}
