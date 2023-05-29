namespace Mitrol.Framework.MachineManagement.Domain.Enums
{
    /// <summary>
    /// Enumerato utilizzato per la definizione dei record di misurazione performance macchina
    /// RTM: Rilevazione TEMPI macchina
    /// RCM: Rilevazione CONSUMI macchina
    /// </summary>
    public enum RMTypeEnum
    {
        Actual = 0,
        Snapshot = 1,
        Program = 2,
        Piece = 3
    }
}
