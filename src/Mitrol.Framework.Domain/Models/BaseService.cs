namespace Mitrol.Framework.Domain.Models
{
    using AutoMapper;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.SignalR;
    using Mitrol.Framework.Domain.SignalR.Gateway;

    public class BaseService : Disposable, IApplicationService
    {
        protected IServiceFactory ServiceFactory { get; }
        protected IMapper Mapper { get; set; }
        protected IUserSession UserSession { get; set; }

        protected IEventHubClient EventHubClient => ServiceFactory.GetService<IEventHubClient>();
        protected IEventLogHubClient EventLogHubClient => ServiceFactory.GetService<IEventLogHubClient>();

        public BaseService(IServiceFactory serviceFactory)
        {
            ServiceFactory = serviceFactory;
            Mapper = serviceFactory.GetService<IMapper>();
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
        }

        public void SetSession(IUserSession userSession)
        {
            UserSession = userSession;
        }
    }
}
