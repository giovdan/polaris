namespace Mitrol.Framework.MachineManagement.Domain.Models.Views
{

    using Mitrol.Framework.Domain.Enums;
    public class SlaveOperationTypeCodeGenerator
    {
        public long ParentOperationId { get; set; }
        public long Id { get; set; }
        public string DisplayName { get; set; }
        public string TextValue { get; set; }
        public decimal? Value { get; set; }
        public int Priority { get; set; }
        public long ParentId { get; set; }
        public long SubParentTypeId { get; set; }
        public string TypeName { get; set; }
        public AttributeKindEnum AttributeKindId { get; set; }
        public int Index { get; set; }
    }
}
