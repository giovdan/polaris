

namespace Mitrol.Framework.Domain.Models
{
    using System;
    using System.Collections.Generic;
    
    public class DictionaryContentComparer<TKey, TValue> : IEqualityComparer<Dictionary<TKey, TValue>>
    {
        private IEqualityComparer<TValue> _valueComparer;

        public static DictionaryContentComparer<TKey, TValue> Default => s_comparer.Value;

        private static readonly Lazy<DictionaryContentComparer<TKey, TValue>> s_comparer
            = new Lazy<DictionaryContentComparer<TKey, TValue>>(Creator);

        private static DictionaryContentComparer<TKey, TValue> Creator() => new DictionaryContentComparer<TKey, TValue>();

        public DictionaryContentComparer(IEqualityComparer<TValue> valueComparer = null)
        {
            _valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
        }

        public bool Equals(Dictionary<TKey, TValue> first, Dictionary<TKey, TValue> second)
        {
#pragma warning disable IDE0041
            var is_first_null = ReferenceEquals(first, null);
            var is_second_null = ReferenceEquals(second, null);
#pragma warning restore IDE0041

            if (is_first_null && is_second_null)
                return true;
            else if (is_first_null || is_second_null)
                return false;
            else
            {
                if (first.Count != second.Count)
                    return false;

                foreach (var pair in first)
                {
                    if (!second.TryGetValue(pair.Key, out var secondValue))
                        return false;//non ci sono chiavi comuni
                    if(!_valueComparer.Equals(pair.Value, secondValue))
                        return false; //quando le chiavi sono comuni non sono uguali i valori
                }
                return true;
            }
        }

        public int GetHashCode(Dictionary<TKey, TValue> obj)
        {
            var hashCode = 792638326;
            foreach (var pair in obj)
            {
                hashCode = hashCode * -1521134295 + EqualityComparer<TKey>.Default.GetHashCode(pair.Key);
                hashCode = hashCode * -1521134295 + EqualityComparer<TValue>.Default.GetHashCode(pair.Value);
            }
            return hashCode;
        }
    }
}
