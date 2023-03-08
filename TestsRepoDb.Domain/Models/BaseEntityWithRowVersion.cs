namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Interfaces;
    using System.ComponentModel.DataAnnotations;

    public class BaseEntityWithRowVersion : BaseEntity, IHasRowVersion
    {
        [ConcurrencyCheck()]
        public string RowVersion { get; set; }
    }
}
