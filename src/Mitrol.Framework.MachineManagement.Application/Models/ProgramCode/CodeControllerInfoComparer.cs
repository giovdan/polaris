namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class CodeControllerInfoComparer : IEqualityComparer<CodeControllerInfo>
    {
        public static CodeControllerInfoComparer Default => s_comparer.Value;

        private static readonly Lazy<CodeControllerInfoComparer> s_comparer
            = new Lazy<CodeControllerInfoComparer>(Creator);

        private static CodeControllerInfoComparer Creator() => new CodeControllerInfoComparer();

        public bool Equals([AllowNull] CodeControllerInfo first, [AllowNull] CodeControllerInfo second)
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
                return first.Id == second.Id
                    && first.ProgressPercentage == second.ProgressPercentage
                    && first.RemainingTime == second.RemainingTime
                    && first.NumberOfTools == second.NumberOfTools
                ;
            }
        }

        public int GetHashCode([DisallowNull] CodeControllerInfo obj)
        {
            var hashCode = 792638326;

            hashCode = hashCode * -1521134295 + EqualityComparer<long>.Default.GetHashCode(obj.Id);
            hashCode = hashCode * -1521134295 + EqualityComparer<int>.Default.GetHashCode(obj.ProgressPercentage);
            hashCode = hashCode * -1521134295 + EqualityComparer<int>.Default.GetHashCode(obj.RemainingTime);
            hashCode = hashCode * -1521134295 + EqualityComparer<int>.Default.GetHashCode(obj.NumberOfTools);

            return hashCode;
        }
    }
}
