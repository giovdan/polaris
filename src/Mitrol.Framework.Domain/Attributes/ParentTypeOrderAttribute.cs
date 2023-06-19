namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class ParentTypeOrderAttribute: Attribute
    {
        public ParentTypeOrderAttribute(ParentTypeEnum parentType, int subParentTypeId = 0, int priority = 999)
        {
            ParentType = parentType;
            SubParentTypeId = subParentTypeId;
            Priority = priority;
        }

        public ParentTypeEnum ParentType { get; set; }
        public int SubParentTypeId { get; set; }
        public int Priority { get; set; }

        
    }

    public sealed class ProgramTypeOrderAttribute : ParentTypeOrderAttribute
    {
        public ProgramTypeOrderAttribute(int subParentTypeId = 0, int priority = 999) :
            base(ParentTypeEnum.ProgramItem, subParentTypeId, priority)
        {

        }
    }

    public sealed class OperationTypeOrderAttribute: ParentTypeOrderAttribute
    {
        public OperationTypeOrderAttribute(int subParentTypeId, int priority = 999):
            base(ParentTypeEnum.OperationType, subParentTypeId, priority)
        {

        }
    }
}
