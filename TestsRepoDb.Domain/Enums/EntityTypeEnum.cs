namespace RepoDbVsEF.Domain.Enums
{
    using System.ComponentModel;

    public enum EntityTypeEnum
    {
        None = 0,
        Supplier = 1,
        Customer = 2,
        Product = 4,
        Order = 8,
        OrderRow = 16
    }
}
