using Mitrol.Framework.Domain.Attributes;

namespace Mitrol.Framework.MachineManagement.Domain.Enums
{
    using Mitrol.Framework.Domain.Enums;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum MaintenanceOwnerEnum
    {
        [Description("Interventi creati da Ficep - solo import")]
        [DatabaseDisplayName("FICEP")]
        [EnumSerializationName("FICEP")]
        Ficep = 1,

        [Description("Interventi creati tramite Polaris")]
        [DatabaseDisplayName("POLARIS")]
        [EnumSerializationName("POLARIS")]
        Polaris = 2
    }
}
