
namespace Mitrol.Framework.MachineManagement.Application.Transformations
{
    using Mitrol.Framework.Domain.Configuration;
    using Mitrol.Framework.Domain.Enums;
    using System.Collections.ObjectModel;

    public class TorchConfigurationCollection : KeyedCollection<UnitEnum, TorchUnitConfiguration>
    {
        protected override UnitEnum GetKeyForItem(TorchUnitConfiguration item)
        {
            return item.Id;
        }
    }
}
