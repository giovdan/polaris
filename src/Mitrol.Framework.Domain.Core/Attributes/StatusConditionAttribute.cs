namespace Mitrol.Framework.Domain.Core.Attributes
{
    using Mitrol.Framework.Domain.Core.Enums;
    using System;

    [AttributeUsage(AttributeTargets.All)]
    public sealed class StatusConditionAttribute : Attribute
    {
        public StatusConditionEnum StatusCondition { get; set; }

        public StatusConditionAttribute(StatusConditionEnum statusCondition)
        {
            StatusCondition = statusCondition;
        }
    }
}
