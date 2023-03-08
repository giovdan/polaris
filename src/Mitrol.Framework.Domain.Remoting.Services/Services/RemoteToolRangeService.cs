namespace Mitrol.Framework.Domain.Remoting.Services
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Production.Interfaces;
    using Mitrol.Framework.Domain.Production.Models;
    using Mitrol.Framework.Domain.Remoting.Services.WebApi;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class RemoteToolRangeService : IRemoteToolRangeService
    {
        public WebApiCaller WebApiCaller { get; }

        public IUserSession UserSession { get; }

        public RemoteToolRangeService(WebApiRestClient remoteData)
        {
            WebApiCaller = remoteData.WebApiCaller;
            UserSession = remoteData.UserSession;
        }

        public IEnumerable<ToolRangeItem> GetToolRanges(ToolRangeItemFilter filter)
        {
            var response = WebApiCaller.Patch(new WebApiRequest<ToolRangeItemFilter>(UserSession)
            {
                Uri = $"OData/ToolRanges/processing",
                Model = filter
            });
            return JsonConvert.DeserializeObject<IEnumerable<ToolRangeItem>>(response.JsonResult);
        }

        public IEnumerable<ToolRangeMasterItem> GetToolMasterIdentifiers(ToolRangeMasterItemFilter filter)
        {
            var response = WebApiCaller.Patch(new WebApiRequest<ToolRangeMasterItemFilter>(UserSession)
            {
                Uri = $"OData/ToolRanges/toolMasters",
                Model = filter
            });
            return JsonConvert.DeserializeObject<IEnumerable<ToolRangeMasterItem>>(response.JsonResult);
        }
    }
}
