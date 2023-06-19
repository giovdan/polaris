using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Mitrol.Framework.Domain.Models
{
    public class RequiredToolCollection : KeyedCollection<long, RequiredTool>, ICollection<RequiredTool>
    {
        private readonly PropertyChangedEventHandler _onPropertyChangedHandler;

        public RequiredToolCollection(PropertyChangedEventHandler onPropertyChangedHandler)
        {
            _onPropertyChangedHandler = onPropertyChangedHandler;
        }

        protected override void ClearItems()
        {
            foreach (var item in this)
            {
                item.PropertyChanged -= _onPropertyChangedHandler;
            }
            base.ClearItems();

        }

        protected override long GetKeyForItem(RequiredTool item) => item.ToolIndex;

        protected override void InsertItem(int index, RequiredTool item)
        {
            if (Dictionary == null || (Dictionary != null && !Dictionary.ContainsKey(item.ToolIndex)))
            {
                item.PropertyChanged += _onPropertyChangedHandler;
                base.InsertItem(index, item);
                item.OnPropertyChanged();
            }
        }

        public IDictionary<long, RequiredTool> GetDictionary() => Dictionary;

    }
}