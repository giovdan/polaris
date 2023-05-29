namespace Mitrol.Framework.MachineManagement.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Enums;
    using System;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [Flags]
    public enum MaintenanceCategoryEnum : int
    {
        [Description("Manutenzione Meccanica")]
        [DatabaseDisplayName("MECHANIC")]
        [EnumSerializationName("MECHANIC")]
        Mechanic = 1,

        [Description("Manutenzione Idraulica")]
        [DatabaseDisplayName("HYDRAULIC")]
        [EnumSerializationName("HYDRAULIC")]
        Hydraulic = 2,

        [Description("Manutenzione elettrica")]
        [DatabaseDisplayName("ELECTRIC")]
        [EnumSerializationName("ELECTRIC")]
        Electric = 4,

        [Description("Manutenzione Software")]
        [DatabaseDisplayName("SOFTWARE")]
        [EnumSerializationName("SOFTWARE")]
        Software = 8
    }
}
