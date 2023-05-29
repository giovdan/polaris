
namespace Mitrol.Framework.MachineManagement.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum MaintenanceActionRepeatEachEnum
    {
        [DatabaseDisplayName("DISABLED")]
        [EnumSerializationName("DISABLED")]
        Disabled = 0,

        [DatabaseDisplayName("WEEKLY")]
        [EnumSerializationName("WEEKLY")]
        Weekly = 2,

        [DatabaseDisplayName("MONTHLY")]
        [EnumSerializationName("MONTHLY")]
        Monthly = 4,

        [DatabaseDisplayName("YEARLY")]
        [EnumSerializationName("YEARLY")]
        Yearly = 8
    }

}
