namespace Mitrol.Framework.Domain.Configuration
{
    using System;
    using System.Collections.Generic;

    public class AxisGroupConfigurationComparer : IEqualityComparer<AxisGroupConfiguration>
    {
        public static AxisGroupConfigurationComparer Default => s_comparer.Value;

        private static readonly Lazy<AxisGroupConfigurationComparer> s_comparer = new Lazy<AxisGroupConfigurationComparer>();

        public AxisGroupConfigurationComparer()
        {
        }

        public bool Equals(AxisGroupConfiguration first, AxisGroupConfiguration second)
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
                return first.LocalizationKey == second.LocalizationKey;
            }
        }

        public int GetHashCode(AxisGroupConfiguration obj)
        {
            var hashCode = 792638326;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(obj.LocalizationKey);
            return hashCode;
        }
    }
}