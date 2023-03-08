namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
    public class OperationInfoAttribute : Attribute
    {
        public ToolTypeEnum[] RelatedToolTypes { get; set; }
        public bool IsSlave { get; set; }
        public OperationTypeEnum OperationType { get; set; }

        public OperationInfoAttribute(bool isSlave = false, params ToolTypeEnum[] toolTypes)
        {
            RelatedToolTypes = toolTypes;
            IsSlave = isSlave;
        }

    }
}
