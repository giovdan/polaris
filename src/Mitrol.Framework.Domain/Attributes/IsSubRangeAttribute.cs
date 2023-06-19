namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;

    public class IsSubRangeAttribute : Attribute
    {
        public IsSubRangeAttribute(SubRangeTypeEnum subRangeType)
        {
            SubRangeType = subRangeType;
        }

        public SubRangeTypeEnum SubRangeType { get; }
    }
}
