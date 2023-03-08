namespace Mitrol.Framework.Domain.Configuration.Models
{
    using System;
    using System.Collections.Generic;

    public class FlagsDictionary<TEnum> : Dictionary<TEnum, bool>
        where TEnum : Enum
    {
        public FlagsDictionary() { }

        public new bool this[TEnum key]
        {
            get
            {
                try
                {
                    return this[key];
                }
                catch (KeyNotFoundException)
                {
                    this[key] = default;
                    return default;
                }
            }
            set => this[key] = value;
        }
    }
}
