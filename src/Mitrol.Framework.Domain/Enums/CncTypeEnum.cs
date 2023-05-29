namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System;
    using System.ComponentModel;

    [Flags]
    public enum CncTypeEnum : int
    {
        [DatabaseDisplayName("FNC")]
        [Description("Fanuc")]
        Fanuc = 1,
        [DatabaseDisplayName("MTL")]
        [Description("Mitrol")]
        Mitrol = 2,
        [DatabaseDisplayName("All")]
        [Description("All")]
        All = Fanuc | Mitrol
    }
}