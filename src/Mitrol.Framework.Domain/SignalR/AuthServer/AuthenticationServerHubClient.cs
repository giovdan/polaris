namespace Mitrol.Framework.Domain.SignalR.AuthServer
{
    using Microsoft.AspNetCore.SignalR.Client;
    using Mitrol.Framework.Domain.Models;
    using System;

    /// <summary>
    /// Concrete SignalR client implementation that publishes or reacts authentication-server's events.
    /// </summary>
    public class AuthenticationServerHubClient : HubClient, IAuthenticationServerHubClient
    {
        /// <summary>
        /// The URL used to contact to the server.
        /// </summary>
        protected override string HubUrl => $"{Environment.GetEnvironmentVariable("MITROLWEBAPI_GATEWAYURL") ?? "http://localhost:4000"}/authServer/events";

        /// <summary>
        /// Notifies the provider that a UserSession notification is to receive.
        /// </summary>
        /// <param name="handler">
        /// The Action to execute receiving the newly created UserSession as a parameter.
        /// </param>
        /// <returns>
        /// A reference to an interface that allows observers to stop receiving notifications before
        /// the provider has finished sending them.
        /// </returns>
        public IDisposable UserSession(Action<UserSession> handler) => HubConnection.On(nameof(IAuthenticationServerHub.UserSession), handler);
    }
}
