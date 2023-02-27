
namespace RepoDbVsEF.Domain.Enums
{
    using System;

    [Flags()]
    public enum AttributeKindEnum
    {
        Number = 1,
        String = 2,
        Enum = 4,
        Bool = 8,
        Date = 16
    }
}
