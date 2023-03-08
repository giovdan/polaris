namespace Mitrol.Framework.Domain.Core.Models.Database
{
    using Mitrol.Framework.Domain.Core.Interfaces;

    public class BaseEntityWithRowVersion : BaseEntity, IHasRowVersion
    {
        public string RowVersion { get; set; }
    }
}
