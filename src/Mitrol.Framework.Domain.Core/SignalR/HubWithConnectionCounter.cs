namespace Mitrol.Framework.Domain.Core.SignalR
{
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A base class for a strongly typed SignalR hub with integrated connection counter.
    /// </summary>
    /// <typeparam name="TClient">The type of client.</typeparam>
    public abstract class HubWithConnectionCounter<TClient> : Hub<TClient> where TClient : class, IHubWithConnectionCounter
    {
        // Counter for established connections
        private static int s_onlineClientsCount = 0;

        /// <summary>
        /// Called when a new connection is established with the hub.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task that represents the asynchronous connect.</returns>
        public override Task OnConnectedAsync()
        {
            // Update the connection counter in a thread-safe way
            Interlocked.Increment(ref s_onlineClientsCount);

            // Notify to all clients online the new value
            ConnectedClientCount(s_onlineClientsCount);

            // Let the hub handle the rest
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Called when a connection with the hub is terminated.
        /// </summary>
        /// <param name="exception">The exception that terminated the connection if any.</param>
        /// <returns>A System.Threading.Tasks.Task that represents the asynchronous disconnect.</returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            // Update the connection counter in a thread-safe way
            Interlocked.Decrement(ref s_onlineClientsCount);

            // Notify to all clients online the new value
            ConnectedClientCount(s_onlineClientsCount);

            // Let the hub handle the rest
            return base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Notify to all clients online the new value of the connection counter.
        /// </summary>
        /// <param name="_">This parameter is ignored server-side.</param>
        /// <returns>A System.Threading.Tasks.Task that represents the asynchronous operation.</returns>
        public Task ConnectedClientCount(int _) => Clients.All.ConnectedClientCount(s_onlineClientsCount);
    }
}
