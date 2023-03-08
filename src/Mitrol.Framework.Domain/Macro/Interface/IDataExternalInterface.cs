
namespace Mitrol.Framework.Domain.Macro
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System.Collections.Generic;

    public interface IDataExternalInterface : IEntityWithAttributeValues<ExternalInterfaceNameEnum>
    {
        Dictionary<ExternalInterfaceNameEnum, object> Attributes { get; }
        Result Add(ExternalInterfaceNameEnum externalInterfaceNameEnum
                    , object value);
    }
}
