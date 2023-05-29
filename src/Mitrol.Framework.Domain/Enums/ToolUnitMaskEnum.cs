namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System;

    [Flags]
    public enum ToolUnitMaskEnum : int
    {
        None = 0,

        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolEnableA)]
        UnitA = 1,

        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolEnableB)]
        UnitB = 2,

        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolEnableC)]
        UnitC = 4,

        [DatabaseDisplayName(DatabaseDisplayNameEnum.ToolEnableD)]
        UnitD = 8,

        [DatabaseDisplayName("All")]
        All = UnitA | UnitB | UnitC | UnitD
    }
}