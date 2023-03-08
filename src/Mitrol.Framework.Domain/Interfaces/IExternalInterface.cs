namespace Mitrol.Framework.Domain.Interfaces
{
    using System.Collections.Generic;

    public interface IEntityWithAttributeValues<T>
    {
        Dictionary<T, object> Attributes { get; }
    }
}
