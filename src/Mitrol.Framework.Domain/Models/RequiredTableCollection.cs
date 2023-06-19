namespace Mitrol.Framework.Domain.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public class RequiredTableCollection: KeyedCollection<long, RequiredToolTable>
    {
        private readonly PropertyChangedEventHandler _onPropertyChangedHandler;

        public bool IsReadOnly => throw new System.NotImplementedException();

        public RequiredTableCollection(PropertyChangedEventHandler onPropertyChangedHandler)
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

        protected override long GetKeyForItem(RequiredToolTable item) => item.Id;

        protected override void InsertItem(int index, RequiredToolTable item)
        {
            if (Dictionary == null || (Dictionary != null && !Dictionary.ContainsKey(item.Id)))
            {
                item.PropertyChanged += _onPropertyChangedHandler;
                base.InsertItem(index, item);
                item.OnPropertyChanged();
            }
        }

        public IDictionary<long, RequiredToolTable> GetDictionary() => Dictionary;

    }
}
