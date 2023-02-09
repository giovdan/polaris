namespace UnitTests.Models
{
    using RepoDbVsEF.Domain.Enums;

    public class AttributeItem
    {
        public ulong Id { get; set; }
        public ulong AttributeDefinitionId { get; set; }
        public AttributeKindEnum AttributeKind { get; set; }
        public AttributeValueItem Value { get; set; }
    }

    public class AttributeValueItem
    {
        public object CurrentValue { get; set; }
        public int CurrentValueId { get; set; }
    }
}
