namespace Mitrol.Framework.Domain.Remoting.Services.SignalR
{
    using Mitrol.Framework.Domain.Remoting.Services.WebApi;
    using Mitrol.Framework.Domain.SignalR.Gateway;

    public class RemoteEventHubClient : EventHubClient
    {
        private RemoteEventHubClient()
        {

        }

        public RemoteEventHubClient(WebApiRestClient remoteData)
            : base($"{remoteData.SetupSection.BaseUri}/events", connectDelay: 0)
        {
                
        }
    }
}
