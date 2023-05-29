namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Enums;

    public class MigratedType : BaseEntity
    {
        public EntityTypeEnum EntityType { get; set; }
        public long ParentTypeId { get; set; }
        public long SubParentTypeId { get; set; }
        public ProcessingTechnologyEnum ProcessingTechnology { get; set; }
    }
}
