

namespace Mitrol.Framework.MachineManagement.Domain.Views
{
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Tool
    {
        public long Id { get; set; }
        public EntityTypeEnum EntityTypeId { get; set; }
        public string HashCode { get; set; }
        [Column(TypeName = "ENUM ('Available','Unavailable','Warning','Alarm','NoIconToDisplay','ToBeDeleted','Original','ModifiedByCustomer','ModifiedByFICEP','ToBeSkipped','Empty','InProgress','Processed','Executed','RQLoad','RQAck','NotAvailable','Aborted','NotReady')")]
        public EntityStatusEnum Status { get; set; }
        public bool IsManaged { get; set; }
        public int ToolManagementId { get; set; }
        public PlantUnitEnum PlantUnitId { get; set; }
        public ToolUnitMaskEnum ToolMask { get; set; }
    }
}
