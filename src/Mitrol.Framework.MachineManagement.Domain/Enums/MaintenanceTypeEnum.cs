namespace Mitrol.Framework.MachineManagement.Domain.Enums
{
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum MaintenanceTypeEnum
    {
        [Description("Unica data di calendario")]
        [EnumSerializationName("CALENDAR")]
        Calendar = 1,

        [Description("Ore Lavorate")]
        [EnumSerializationName("WORKINGHOURS")]
        WorkingHours = 2
    }
}
