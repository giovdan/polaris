using Mitrol.Framework.Domain.Enums;
using Mitrol.Framework.Domain.Models;
using System.Collections.Generic;

namespace Mitrol.Framework.Domain.Macro
{
    public class ToolFilter : IToolManagementFilter
    {

        public ToolFilter(ToolTypeEnum toolTypeEnum)
        {
            ToolType = toolTypeEnum;
            Attributes = new Dictionary<ExternalInterfaceNameEnum, object>();
        }

        public Dictionary<ExternalInterfaceNameEnum, object> Attributes { get; set; }

        public ToolTypeEnum ToolType { get; private set; }

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
