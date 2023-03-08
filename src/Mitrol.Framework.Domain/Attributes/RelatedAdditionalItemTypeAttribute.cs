namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;
    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class RelatedAdditionalItemTypeAttribute: Attribute
    {
        public OperationTypeEnum AdditionalItemType { get; set; }

        public RelatedAdditionalItemTypeAttribute(OperationTypeEnum additionalItemType)
        {
            AdditionalItemType = additionalItemType;
        }
    }
}
