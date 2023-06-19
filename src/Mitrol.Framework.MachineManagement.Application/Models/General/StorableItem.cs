namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;

    public class StorableItem : Dirtyable, IStorable
    {
        [JsonIgnore]
        public IDataCollector DataCollector { get; internal set; }

        public StorableItem(IDataCollector dataCollector)
        {
            DataCollector = dataCollector;
            DataCollector?.Subscribe(this);
        }

        public virtual bool Save()
        {
            IsDirty = false;
            return true;
        }

        public virtual bool ShouldSave()
        {
            return IsDirty;
        }
    }
}
