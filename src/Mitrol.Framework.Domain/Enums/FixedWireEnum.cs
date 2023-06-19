namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Disposizione del filo fisso dell'impianto (osservando la macchina a cavallo della motrice):
    /// </summary>
    public enum FixedWireEnum
    {
        /// <summary>
        /// Filo fisso a destra
        /// </summary>
        //OnRight = 0,
        D0 = 0,

        /// <summary>
        /// Filo fisso a sinistra (X speculari)
        /// </summary>
        //OnLeft_XSpecular = 1,
        D1 = 1,

        /// <summary>
        /// Filo fisso a destra (X e Y speculari)
        /// </summary>
        //OnRight_XYSpecular = 2,
        D2 = 2,

        /// <summary>
        /// Filo fisso a sinistra (Y speculari)
        /// </summary>
        //OnLeft_YSpecular = 3,
        D3 = 3,
    }
}