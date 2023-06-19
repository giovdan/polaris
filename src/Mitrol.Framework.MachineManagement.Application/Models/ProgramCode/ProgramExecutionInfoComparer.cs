namespace Mitrol.Framework.MachineManagement.Application.Models.ProgramCode
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public class ProgramExecutionInfoComparer : IEqualityComparer<ProgramExecutionInfo>
    {
        public static ProgramExecutionInfoComparer Default => s_comparer.Value;

        private static readonly Lazy<ProgramExecutionInfoComparer> s_comparer
            = new Lazy<ProgramExecutionInfoComparer>(Creator);

        private static ProgramExecutionInfoComparer Creator() => new ProgramExecutionInfoComparer();

        public bool Equals([AllowNull] ProgramExecutionInfo first, [AllowNull] ProgramExecutionInfo second)
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
                return first.CurrentUnitIndex == second.CurrentUnitIndex
                    //&& JogOverrideValueComparer<short>.Default.Equals(first.AxesOverride, second.AxesOverride) TODO
                    && first.Units.SequenceEqual(second.Units, UnitDataComparer.Default)
                    && first.Overlays.SequenceEqual(second.Overlays, GraphicOverlayComparer.Default)
                ;
            }
        }

        public int GetHashCode([DisallowNull] ProgramExecutionInfo obj)
        {
            var hashCode = 792638326;
            // TODO
            //hashCode = hashCode * -1521134295 + JogOverrideValueComparer<short>.Default.GetHashCode(obj.AxesOverride);
            hashCode = hashCode * -1521134295 + EqualityComparer<int>.Default.GetHashCode(obj.CurrentUnitIndex);
            foreach (var unit in obj.Units)
            {
                hashCode = hashCode * -1521134295 + UnitDataComparer.Default.GetHashCode(unit);
            }

            return hashCode;
        }
    }
}
