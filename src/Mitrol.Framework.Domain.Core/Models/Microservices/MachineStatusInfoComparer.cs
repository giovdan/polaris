namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Core.Enums;
    using System;
    using System.Collections.Generic;

    public class MachineStatusInfoComparer : IEqualityComparer<MachineStatusInfo>
    {
        public static MachineStatusInfoComparer Default => s_comparer.Value;

        private static readonly Lazy<MachineStatusInfoComparer> s_comparer
            = new Lazy<MachineStatusInfoComparer>(Creator);

        private static MachineStatusInfoComparer Creator() => new ();

        public bool Equals(MachineStatusInfo first, MachineStatusInfo second)
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
                return first.MachineState == second.MachineState
                    && first.IsoOperationsEnabled == second.IsoOperationsEnabled
                    && first.ProcessingEnabled == second.ProcessingEnabled;
            }
        }

        public int GetHashCode(MachineStatusInfo obj)
        {
            if (obj is null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            var hashCode = 792638326;
            hashCode = hashCode * -1521134295 + EqualityComparer<MachineStateEnum>.Default.GetHashCode(obj.MachineState);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(obj.IsoOperationsEnabled);
            hashCode = hashCode * -1521134295 + EqualityComparer<bool>.Default.GetHashCode(obj.ProcessingEnabled);
            return hashCode;
        }
    }
}
