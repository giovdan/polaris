namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Enumerato che definisce il tipo di cesoia
    /// </summary>
    public enum ShearMachineTypeEnum
    {
        /// <summary>
        /// Non configurata (DEFAULT)
        /// </summary>
        None = 0,

        /// <summary>
        /// Cesoia standard
        /// </summary>
        Standard = 1,

        /// <summary>
        /// Cesoia standard + cesoia per piatti in linea (504PS)
        /// </summary>
        StandardAndFlat = 2,

        /// <summary>
        /// Cesoia standard + cesoia per piatti con asse W in linea (TipoD3-6)
        /// </summary>
        StandardAndFlatWithAXW = 3,
    }
}
