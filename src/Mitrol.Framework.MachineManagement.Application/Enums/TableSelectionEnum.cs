namespace Mitrol.Framework.MachineManagement.Application.Enums
{
    using Mitrol.Framework.Domain.Enums;
    // Tipologie di tabelle
    public enum TableSelectionEnum
    {
        [EnumCustomName("TOOLTABLES")]
        ToolTables = 1,
        [EnumCustomName("MATERIALCODES")]
        MaterialCodes = 2,
        [EnumCustomName("TOOLHOLDERS")]
        ToolHolders = 3,
        [EnumCustomName("PROFILES")]
        Profiles =4
    }
}
