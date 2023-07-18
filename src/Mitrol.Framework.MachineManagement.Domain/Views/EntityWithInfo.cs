using Mitrol.Framework.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mitrol.Framework.MachineManagement.Domain.Views
{
    public class EntityWithInfo
    {
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public EntityTypeEnum EntityTypeId { get; set; }
        public long SecondaryKey { get; set; }
        public EntityStatusEnum Status { get; set; }
        public int IsLinked { get; set; }
        public string  HashCode { get; set; }
        public long ExecutedQuantity { get; set; }
        public long TotalQuantity { get; set; }
        public long ScheduledQuantity { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
