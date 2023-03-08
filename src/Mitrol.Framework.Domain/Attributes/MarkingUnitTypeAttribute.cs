namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;

    [AttributeUsage(AttributeTargets.All)]
    public class MarkingUnitTypeAttribute: Attribute
    {
        public MarkingUnitTypeEnum MarkingUnitType { get; private set; }
        public MarkingUnitTypeAttribute(MarkingUnitTypeEnum markingUnitType)
        {
            MarkingUnitType = markingUnitType;
        }
    }
}
