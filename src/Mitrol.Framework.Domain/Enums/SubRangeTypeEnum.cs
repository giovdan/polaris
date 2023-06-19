namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System;
    using System.ComponentModel;

    [Flags]
    public enum SubRangeTypeEnum
    {
        None = 0,
        [DatabaseDisplayName("BV")]
        [Description("Bevel")]    
        Bevel=1,
        [DatabaseDisplayName("TH")]
        [Description("True Hole")]
        TrueHole=2,
        [DatabaseDisplayName("MK")]
        [Description("Mark")]
        Mark=4,
        [DatabaseDisplayName("ALL")]
        [Description("Tutti")]
        All = Bevel | TrueHole | Mark
    }
}
