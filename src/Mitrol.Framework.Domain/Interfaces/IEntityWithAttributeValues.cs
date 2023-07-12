namespace Mitrol.Framework.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Enums;
    using System.Collections.Generic;

    public interface IEntityWithAttributeValuesAndIdentifiers : IEntityWithAttributeValues
    {
        Dictionary<DatabaseDisplayNameEnum, object> Identifiers { get; }
    }

    public interface IEntityWithAttributeValues
    {
        Dictionary<DatabaseDisplayNameEnum, object> Attributes { get; }
    }

}
