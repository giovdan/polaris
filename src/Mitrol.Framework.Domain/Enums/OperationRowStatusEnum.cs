namespace Mitrol.Framework.Domain.Enums
{
    /// <summary>
    /// Stato riga operazione in cache
    /// </summary>
    public enum OperationRowStatusEnum
    {
        UnChanged = 0,
        Added = 1,
        Removed = 2,
        Updated = 3,
        AttributeUpdated = 4,
        FullUpdated = 5
    }
}
