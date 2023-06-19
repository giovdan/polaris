namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class AppliesToProgramCodeLineAttribute : Attribute
    {
        public LineTypeEnum LineType { get; private set; }
        public int Priority { get; set; }

        public AppliesToProgramCodeLineAttribute(LineTypeEnum lineType
                                            , int priority = 999)
        {
            LineType = lineType;
            Priority = priority;
        }
    }

}