namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;

    /// <summary>
    /// Related filter for ToolRangeType Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public sealed class RelatedFilterAttribute : Attribute
    {
        public RelatedFilterAttribute(DatabaseDisplayNameEnum displayName
                        , bool isMainFilter=false
                        , int priority = 999)
        {
            DisplayName = displayName;
            IsMainFilter = isMainFilter;
            Priority = priority;
        }

        public DatabaseDisplayNameEnum DisplayName { get; set; }
        public bool IsMainFilter { get; set; }
        public int Priority { get; set; }
    }


  
}

