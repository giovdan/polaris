namespace Mitrol.Framework.MachineManagement.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.MachineManagement.Domain.Attributes;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum MaintenanceStatusEnum
    {
        NotDefined = 0,
        [Description("Scaduto")]
        [DatabaseDisplayName("EXPIRED")]
        [EnumSerializationName("EXPIRED")]
        [StatusColor(StatusColorEnum.Orange)]
        Expired = 1,

        [Description("In Scadenza")]
        [DatabaseDisplayName("EXPIRING")]
        [EnumSerializationName("EXPIRING")]
        [StatusColor(StatusColorEnum.Blue)]
        Expiring = 2,

        [Description("Attivo")]
        [DatabaseDisplayName("ACTIVE")]
        [EnumSerializationName("ACTIVE")]
        [StatusColor(StatusColorEnum.Green)]
        Active = 4,

        [Description("Disattivo")]
        [DatabaseDisplayName("DEACTIVATED")]
        [EnumSerializationName("DEACTIVATED")]
        [StatusColor(StatusColorEnum.Grey)]
        DeActivated = 8,

        [Description("Terminato")]
        [DatabaseDisplayName("FINISHED")]
        [EnumSerializationName("FINISHED")]
        [StatusColor(StatusColorEnum.Red)]
        Finished = 16

    }
}
