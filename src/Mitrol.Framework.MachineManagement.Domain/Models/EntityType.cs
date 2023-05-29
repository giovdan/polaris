
namespace Mitrol.Framework.MachineManagement.Domain.Models.General
{
    using Mitrol.Framework.Domain.Core.Models;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("EntityType")]
    public class EntityType: BaseEntity
    {
        public string DisplayName { get; set; }
        public bool IsManaged { get; set; }
    }
}
