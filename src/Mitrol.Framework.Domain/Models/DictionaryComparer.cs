namespace Mitrol.Framework.Domain.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class KeyValuePairCpmparer<TKey, TValue> : IEqualityComparer<KeyValuePair<TKey, TValue>>
    {
        private IEqualityComparer<TValue> _valueComparer;

        public static KeyValuePairCpmparer<TKey, TValue> Default => s_comparer.Value;

        private static readonly Lazy<KeyValuePairCpmparer<TKey, TValue>> s_comparer
            = new Lazy<KeyValuePairCpmparer<TKey, TValue>>(Creator);

        private static KeyValuePairCpmparer<TKey, TValue> Creator() => new KeyValuePairCpmparer<TKey, TValue>();

        public KeyValuePairCpmparer(IEqualityComparer<TValue> valueComparer = null)
        {
            _valueComparer = valueComparer ?? EqualityComparer<TValue>.Default;
        }

        public bool Equals(KeyValuePair<TKey, TValue> x, KeyValuePair<TKey, TValue> y)
        {
            throw new NotImplementedException();
        }

        public int GetHashCode(KeyValuePair<TKey, TValue> obj)
        {
            throw new NotImplementedException();
        }
    }

    public class DictionaryComparer<TKey, TValue>
        : IEqualityComparer<Dictionary<TKey, TValue>>
        , IEqualityComparer<KeyValuePair<TKey, TValue>>
    {
        private IEqualityComparer<TValue> _valueComparer;

        public static DictionaryComparer<TKey, TValue> Default => s_comparer.Value;

        private static readonly Lazy<DictionaryComparer<TKey, TValue>> s_comparer
            = new Lazy<DictionaryComparer<TKey, TValue>>(Creator);

        private static DictionaryComparer<TKey, TValue> Creator() => new DictionaryComparer<TKey, TValue>();

        public DictionaryComparer(IEqualityComparer<TValue> valueComparer = null)
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
                if (!first.Keys.SequenceEqual(second.Keys))
                    return false;

                foreach (var pair in first)
                    if (!_valueComparer.Equals(pair.Value, second[pair.Key]))
                        return false;

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

        public bool Equals(KeyValuePair<TKey, TValue> first, KeyValuePair<TKey, TValue> second)
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
                return EqualityComparer<TKey>.Default.Equals(first.Key, second.Key);
            }
        }
        public int GetHashCode(KeyValuePair<TKey, TValue> obj)
        {
            var hashCode = 792638326;
            hashCode = hashCode * -1521134295 + EqualityComparer<TKey>.Default.GetHashCode(obj.Key);
            return hashCode;
        }
    }
}
