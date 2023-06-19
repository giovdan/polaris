namespace Mitrol.Framework.Domain.Core.SignalR.Gateway
{
    using Mitrol.Framework.Domain.SignalR.Gateway;
    using System;

    /// <summary>
    /// Concrete SignalR client implementation that publishes or reacts to events.
    /// </summary>
    public class EventHubClient : EventHubClientBase, IEventHubClient
    {
        /// <summary>
        /// Default constructor that initializes the hub connection.
        /// </summary>
        public EventHubClient() : base() { }

        protected override string HubUrl => $"{Environment.GetEnvironmentVariable("MITROLWEBAPI_GATEWAYURL") ?? "http://localhost:4000"}/events";
    }
}
