
namespace Mitrol.Framework.Domain.Macro
{
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ExternalBaseData : IDataExternalInterface
    {
        [JsonProperty("Attributes")]
        public Dictionary<ExternalInterfaceNameEnum, object> Attributes { get; set; }

        public ExternalBaseData()
        {
            Attributes = new Dictionary<ExternalInterfaceNameEnum, object>();
        }

        public Result Add(ExternalInterfaceNameEnum externalInterfaceNameEnum, object value)
        {
            if (Attributes.ContainsKey(externalInterfaceNameEnum))
            {
                Attributes.Remove(externalInterfaceNameEnum);
            }
            Attributes.Add(externalInterfaceNameEnum, value);
            return Result.Ok();
        }
    }
}
