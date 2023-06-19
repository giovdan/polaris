namespace Mitrol.Framework.Domain.Configuration.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class FlagsDictionary<TEnum> : IReadOnlyDictionary<TEnum, bool>
        where TEnum : Enum
    {
        private readonly IDictionary<TEnum, bool> _dictionary;

        public FlagsDictionary() { _dictionary = new Dictionary<TEnum, bool>(); }

        [JsonConstructor]
        public FlagsDictionary(IDictionary<TEnum, bool> dictionary)
        { 
            _dictionary = dictionary;
        }

        public bool this[TEnum key]
        {
            get
            {
                try
                {
                    return _dictionary[key];
                }
                catch (KeyNotFoundException)
                {
                    _dictionary[key] = default;
                    return default;
                }
            }
            set => _dictionary[key] = value;
        }

        bool IReadOnlyDictionary<TEnum,bool>.this[TEnum key]
        {
            get
            {
                try
                {
                    return _dictionary[key];
                }
                catch (KeyNotFoundException)
                {
                    _dictionary[key] = default;
                    return default;
                }
            }
        }

        public IEnumerable<TEnum> Keys => _dictionary.Keys;

        public IEnumerable<bool> Values => _dictionary.Values;

        public int Count => _dictionary.Count;

        public bool ContainsKey(TEnum key) => _dictionary.ContainsKey(key);

        public IEnumerator<KeyValuePair<TEnum, bool>> GetEnumerator() => _dictionary.GetEnumerator() ?? default;

        public bool TryGetValue(TEnum key, out bool value) => _dictionary.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
