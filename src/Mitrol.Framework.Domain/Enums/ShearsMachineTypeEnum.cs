namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Enumerato che definisce il tipo di cesoia
    /// </summary>
    public enum ShearsMachineTypeEnum
    {
        /// <summary>
        /// Non configurata (DEFAULT)
        /// </summary>
        None = 0,
        
        /// <summary>
        /// Cesoia tradizionale (profili angolari e/o piatti)
        /// </summary>
        Shears,

        /// <summary>
        /// Cesoia per piatti con asse W (impianti TipoD)
        /// </summary>
        ShearsWithAxesW,
    }
}
