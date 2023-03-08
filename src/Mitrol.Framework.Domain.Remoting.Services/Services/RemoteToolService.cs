namespace Mitrol.Framework.Domain.Remoting.Services
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Production.Interfaces;
    using Mitrol.Framework.Domain.Production.Models;
    using Mitrol.Framework.Domain.Remoting.Services.WebApi;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class RemoteToolService : IRemoteToolService
    {
        public WebApiCaller WebApiCaller { get; }

        public IUserSession UserSession { get; }

        public RemoteToolService(WebApiRestClient remoteData)
        {
            WebApiCaller = remoteData.WebApiCaller;
            UserSession = remoteData.UserSession;
        }

        public IEnumerable<ToolItem> GetToolIdentifiers()
        {
            return GetToolIdentifiers(new ToolItemIdentifiersFilter());
        }

        public IEnumerable<ToolItem> GetToolIdentifiers(ToolItemIdentifiersFilter filter)
        {
            var response = WebApiCaller.Patch(new WebApiRequest<ToolItemIdentifiersFilter>(UserSession)
            {
                Uri = $"OData/Tools/Identifiers",
                Model = filter
            });
            return JsonConvert.DeserializeObject<IEnumerable<ToolItem>>(response.JsonResult);
        }

        public ToolItem GetTool(ToolItemFilter filter)
        {
            var response = WebApiCaller.Patch(new WebApiRequest<ToolItemFilter>(UserSession)
            {
                Uri = $"Tools/item",
                Model = filter
            });
            return JsonConvert.DeserializeObject<ToolItem>(response.JsonResult);
        }
    }
}
