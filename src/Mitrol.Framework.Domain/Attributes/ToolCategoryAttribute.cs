namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;

    [AttributeUsage(AttributeTargets.All)]
    public class ToolCategoryAttribute : Attribute
    {
        public ToolCategoryEnum ToolCategory { get; private set; }

        public ToolCategoryAttribute()
        {
            ToolCategory = ToolCategoryEnum.Generic;
        }

        public ToolCategoryAttribute(ToolCategoryEnum toolCategory)
        {
            ToolCategory = toolCategory;
        }
    }
}