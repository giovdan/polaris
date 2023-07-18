namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using Mitrol.Framework.Domain.Core.Models.Microservices;

    public class StockItemFilter : BaseEntityFilter
    {
        public bool IsCopy { get; set; }

        public StockItemFilter()
        {
            IsCopy = false;
        }
    }
}
