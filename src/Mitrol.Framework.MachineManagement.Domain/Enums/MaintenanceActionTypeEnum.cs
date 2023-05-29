using Mitrol.Framework.Domain.Attributes;

namespace Mitrol.Framework.MachineManagement.Domain.Enums
{
    public enum MaintenanceActionTypeEnum
    {
        [DatabaseDisplayName("CREATED")]
        Created = 1,
        [DatabaseDisplayName("COMPLETED")]
        Completed = 2,
        [DatabaseDisplayName("RESCHEDULED")]
        Rescheduled = 3,
        [DatabaseDisplayName("ACTIVATED")]
        Activated = 4,
        [DatabaseDisplayName("DEACTIVATED")]
        Deactivated = 5,
        [DatabaseDisplayName("REMOVED")]
        Removed = 6,
        [DatabaseDisplayName("IMPORTED")]
        Imported = 7

    }
}
