namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Enums;

    public class SetupSlotManagementItem
    {
        public UnitEnum Unit { get; set; }
        public short Slot { get; set; }
        public bool Enable { get; set; }
    }
}
