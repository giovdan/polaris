namespace Mitrol.Framework.Domain.SignalR
{
    using Microsoft.AspNetCore.SignalR.Client;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Concrete SignalR client implementation that publishes or reacts EventLog's events.
    /// </summary>
    public class NotesHubClient : HubClient, INotesHubClient
    {
        /// <summary>
        /// The URL used to contact to the server.
        /// </summary>
        protected override string HubUrl => $"{Environment.GetEnvironmentVariable("MITROLWEBAPI_GATEWAYURL") ?? "http://localhost:4000"}/notes/events";

        /// <summary>
        /// Raises a UserRemoved event.
        /// </summary>
        /// <param name="userId">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public Task UserRemoved(long userId) => HubConnection.InvokeAsync(nameof(INotesHub.UserRemoved), userId);
    }
}
