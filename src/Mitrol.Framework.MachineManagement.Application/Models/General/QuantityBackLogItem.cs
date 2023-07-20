namespace Mitrol.Framework.MachineManagement.Application.Models.General
{
    public class QuantityBackLogItem
    {
        public long EntityId { get; set; }
        public int TotalQuantity { get; set; }
        /// <summary>
        /// Quantità eseguite
        /// Qf
        /// </summary>
        public int ExecutedQuantity { get; set; }
        /// <summary>
        /// Quantità da caricare(<= TotalQuantity)
        /// </summary>
        public int QuantityToBeLoaded { get; set; }
        /// <summary>
        /// Quantità caricate(<= QuantityTobeLoaded)
        /// </summary>
        public int QuantityLoaded { get; set; }
    }
}
