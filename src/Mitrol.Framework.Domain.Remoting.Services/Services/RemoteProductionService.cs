namespace Mitrol.Framework.Domain.Remoting.Services
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Production.Interfaces;
    using Mitrol.Framework.Domain.Production.Models;
    using Mitrol.Framework.Domain.Remoting.Services.WebApi;
    using System;
    using System.Collections.Generic;

    public class RemoteProductionService : IRemoteProductionService
    {
        public WebApiCaller WebApiCaller { get; }

        public IUserSession UserSession { get; }

        public RemoteProductionService(WebApiRestClient remoteData)
        {
            WebApiCaller = remoteData.WebApiCaller;
            UserSession = remoteData.UserSession;
        }

        public Result<ProductionItem> GetProductionItem(long id, MeasurementSystemEnum conversionSystem = MeasurementSystemEnum.MetricSystem)
        {
            return WebApiCaller.Get<ProductionItem>($"ProductionList/{id}", UserSession);
        }


        public Result<ProductionItem> GetProgramItem(long id)
        {
            return WebApiCaller.Get<ProductionItem>($"ProductionList/{id}", UserSession);
        }


        public Result UpdateProductionRowStatus(long productionRowId, ProductionRowStatusEnum productionRowStatus)
        {
            throw new NotImplementedException();
        }

        public Result UpdateProductionRowsStatus(IEnumerable<long> productionRowIds, ProductionRowStatusEnum productionRowStatus)
        {
            throw new NotImplementedException();
        }

        public Result<ProductionItem> GetProductionItem(long id)
        {
            return WebApiCaller.Get<ProductionItem>($"ProductionList/{id}", UserSession);
        }
    }
}
