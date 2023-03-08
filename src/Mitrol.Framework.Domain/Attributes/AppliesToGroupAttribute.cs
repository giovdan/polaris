namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [AttributeUsage(AttributeTargets.All)]
    public partial class AppliesToGroupAttribute : Attribute
    {
        public List<GroupEnum> RelatedGroups { get; set; }

        public AppliesToGroupAttribute(params GroupEnum[] relatedGroups)
        {
            RelatedGroups = relatedGroups.ToList();
        }
    }
}
