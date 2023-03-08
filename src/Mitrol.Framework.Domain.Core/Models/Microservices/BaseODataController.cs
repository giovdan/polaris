namespace Mitrol.Framework.Domain.Core.Models
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.OData.Query;
    using Microsoft.AspNetCore.OData.Routing.Controllers;
    using Microsoft.Extensions.Configuration;
    using Mitrol.Framework.Domain.Core.Helpers;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;

    [ApiController]
    [Consumes("application/json"), Produces("application/json")]
    [Route("api/v{version:ApiVersion}/OData/[controller]")]
    [EnableQuery(EnsureStableOrdering = false)]
    public class BaseODataController : ODataController
    {
        public IConfiguration Configuration { get; private set; }
        public IServiceFactory ServiceFactory { get; }

        public BaseODataController(IConfiguration configuration, IServiceFactory serviceFactory)
        {
            Configuration = configuration;
            ServiceFactory = serviceFactory;
        }

        public UserSession CurrentSession => WebApiHelper.Instance.GetCurrentSession(HttpContext);

        public MeasurementSystemEnum CurrentSessionConversionSystem => CurrentSession?.ConversionSystem.ToRealConversionSystem()
                                                                       ?? MeasurementSystemEnum.MetricSystem; 
    }
}