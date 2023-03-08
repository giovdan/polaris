namespace Mitrol.Framework.Domain.Core.Models
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Mitrol.Framework.Domain.Core.Helpers;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;

    [ApiController]
    [Consumes("application/json"), Produces("application/json")]
    [Route("api/v{version:ApiVersion}/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        public IConfiguration Configuration { get; }
        public IServiceFactory ServiceFactory { get; }

        public BaseController(IConfiguration configuration, IServiceFactory serviceFactory)
        {
            Configuration = configuration;
            ServiceFactory = serviceFactory;
        }

        public UserSession CurrentSession => WebApiHelper.Instance.GetCurrentSession(HttpContext);
        public Result<UserSession> ResetCurrentSession() => WebApiHelper.Instance.ResetCurrentSession(HttpContext);

        public MeasurementSystemEnum CurrentSessionConversionSystem => 
                CurrentSession?.ConversionSystem.ToRealConversionSystem() ?? MeasurementSystemEnum.MetricSystem;
    }
    
    public abstract class BaseBootController : BaseController
    {
        public BaseBootController(IConfiguration configuration, IServiceFactory serviceFactory) : base(configuration, serviceFactory)
        {
        }

        public abstract IActionResult Boot();
    }
}