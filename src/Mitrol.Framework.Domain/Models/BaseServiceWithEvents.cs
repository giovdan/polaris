namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.SignalR;
    using Mitrol.Framework.Domain.SignalR.Gateway;

    public class BaseServiceWithEvents: BaseService
    {
        public BaseServiceWithEvents(IServiceFactory serviceFactory) : base(serviceFactory)
        {

        }

        protected IEventHubClient EventHubClient => ServiceFactory.GetService<IEventHubClient>();
        protected IEventLogHubClient EventLogHubClient => ServiceFactory.GetService<IEventLogHubClient>();
    }
}
