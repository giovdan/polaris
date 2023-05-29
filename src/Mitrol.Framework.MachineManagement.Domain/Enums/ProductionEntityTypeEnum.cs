using System;

namespace Mitrol.Framework.MachineManagement.Domain.Enums
{
    [Flags()]
    public enum ProductionEntityTypeEnum
    {
        /// <summary>
        /// Elemento della lista Programmi
        /// </summary>
        Program = 1,

        /// <summary>
        /// Pezzo a misura
        /// </summary>
        Piece = 2,

        /// <summary>
        /// Elemento dello Stock
        /// </summary>
        Stock = 4,

        ///<summary>
        /// Linea di produzione
        ///</summary>
        ProductionRow = 8
    }
}
