namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class WorkTypesAttribute: Attribute
    {
        public ToolWorkTypeEnum[] ToolWorkTypes { get; set; }

        public WorkTypesAttribute(params ToolWorkTypeEnum[] toolWorkTypes)
        {
            ToolWorkTypes = toolWorkTypes;
        }
    }
}
