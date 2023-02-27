
namespace RepoDbVsEF.Domain.Enums
{
    using HotChocolate;
    using RepoDbVsEF.Domain.Attributes;
    using RepoDbVsEF.Domain.Core;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum AttributeDefinitionEnum
    {
        [GraphQLName("FirstName")]
        [AttributeInfo(entityType: EntityTypeEnum.Customer, attributeKind: AttributeKindEnum.String)]
        FirstName = 1,
        [AttributeInfo(entityType: EntityTypeEnum.Customer, attributeKind: AttributeKindEnum.String)]
        LastName = 2,
        [AttributeInfo(entityType: EntityTypeEnum.Order, attributeKind: AttributeKindEnum.String)]
        TrackCode = 3,
        [AttributeInfo(entityType: EntityTypeEnum.Customer | EntityTypeEnum.Supplier, attributeKind: AttributeKindEnum.String)]
        Address = 4,
        [AttributeInfo(entityType: EntityTypeEnum.Customer | EntityTypeEnum.Supplier, attributeKind: AttributeKindEnum.String)]
        PhoneNumber = 5,
        [AttributeInfo(entityType: EntityTypeEnum.Customer | EntityTypeEnum.Supplier, attributeKind: AttributeKindEnum.String)]
        Email = 6,
        [AttributeInfo(entityType: EntityTypeEnum.Order, attributeKind: AttributeKindEnum.Date)]
        DeliveryDate = 7,
        [AttributeInfo(entityType: EntityTypeEnum.Order, attributeKind: AttributeKindEnum.Date)]
        SendDate = 8,
        [AttributeInfo(entityType: EntityTypeEnum.Order | EntityTypeEnum.OrderRow | EntityTypeEnum.Product, attributeKind: AttributeKindEnum.Number)]
        Price = 9,
        [AttributeInfo(entityType: EntityTypeEnum.Supplier, attributeKind: AttributeKindEnum.String)]
        Name = 10,
        [AttributeInfo(entityType: EntityTypeEnum.Supplier | EntityTypeEnum.Customer, attributeKind: AttributeKindEnum.String)]
        TaxCode = 11,
        [AttributeInfo(entityType: EntityTypeEnum.Product, attributeKind: AttributeKindEnum.String)]
        SerialNumber = 12,
        [AttributeInfo(entityType: EntityTypeEnum.Product | EntityTypeEnum.OrderRow, attributeKind: AttributeKindEnum.Number)]
        Quantity = 13
    }
}
