namespace Mitrol.Framework.MachineManagement.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum MaintenanceDeltaModesEnum
    {
        [DatabaseDisplayName("ONEDAY")]
        [EnumSerializationName("ONEDAY")]
        OneDay = 1,
        [DatabaseDisplayName("TWODAYS")]
        [EnumSerializationName("TWODAYS")]
        TwoDays = 2,
        [DatabaseDisplayName("THREEDAYS")]
        [EnumSerializationName("THREEDAYS")]
        ThreeDays = 3,
        [DatabaseDisplayName("ONEWEEK")]
        [EnumSerializationName("ONEWEEK")]
        OneWeek = 7
    }

}
