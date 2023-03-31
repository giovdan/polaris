namespace Mitrol.Framework.Domain.Core.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    [Obsolete]
    public class MasterGroupAttribute: Attribute
    {
        public List<BuiltInGroupEnum> MastersGroup { get; internal set; }

        public MasterGroupAttribute(params BuiltInGroupEnum[] masterGroups)
        {
            MastersGroup = masterGroups.ToList();
        }
    }
}
