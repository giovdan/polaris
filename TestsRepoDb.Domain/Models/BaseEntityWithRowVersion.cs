namespace RepoDbVsEF.Domain.Models
{
    using RepoDbVsEF.Domain.Interfaces;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class BaseEntityWithRowVersion : BaseEntity, IHasRowVersion
    {
        [ConcurrencyCheck()]
        public long RowVersion { get; set; }
    }
}
