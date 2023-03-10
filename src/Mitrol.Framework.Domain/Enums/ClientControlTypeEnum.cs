namespace Mitrol.Framework.Domain.Enums
{
    using System;

    [Flags]
    public enum ClientControlTypeEnum : int
    {
        None = 1,
        Edit = 2,
        Label = 4,
        Combo = 8,
        ListBox = 16,
        Check = 32,
        Override = 64,
        Image = 128,
        MultiValue = 256
    }
}
