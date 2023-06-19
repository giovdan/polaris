namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Tipologia dei nodi EtherCAT configurabili
    /// </summary>
    public enum EtherCATTypeEnum : uint
    {
        DS301 = 0x12D,      // Nodo generico DS301
        DRIVE = 0x2       // Azionamento
    }
}
