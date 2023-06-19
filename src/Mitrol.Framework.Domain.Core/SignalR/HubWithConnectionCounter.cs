namespace Mitrol.Framework.Domain.Core.SignalR
{
    using Microsoft.AspNetCore.SignalR;
    using System;
    using System.Collections.Concurrent;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A base class for a strongly typed SignalR hub with integrated connection counter.
    /// </summary>
    /// <typeparam name="TClient">The type of client.</typeparam>
    public abstract class HubWithConnectionCounter<TClient> : Hub<TClient> where TClient : class, IHubWithConnectionCounter
    {
        // Counter for established connections
        private static ConcurrentDictionary<Type, int> s_dictionary = new();

        /// <summary>
        /// Called when a new connection is established with the hub.
        /// </summary>
        /// <returns>A System.Threading.Tasks.Task that represents the asynchronous connect.</returns>
        public override Task OnConnectedAsync()
        {
            // Update the connection counter in a thread-safe way
            var count = s_dictionary.AddOrUpdate(typeof(TClient), 1, (_, value) => Interlocked.Increment(ref value));
            
            // Notify to all clients online the new value
            ConnectedClientCount(count);

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
            var count = s_dictionary.AddOrUpdate(GetType(), 0, (_, value) => Interlocked.Decrement(ref value));

            // Notify to all clients online the new value
            ConnectedClientCount(count);

            // Let the hub handle the rest
            return base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Notify to all clients online the new value of the connection counter.
        /// </summary>
        /// <param name="_">This parameter is ignored server-side.</param>
        /// <returns>A System.Threading.Tasks.Task that represents the asynchronous operation.</returns>
        public Task ConnectedClientCount(int _) => Clients.All.ConnectedClientCount(s_dictionary[typeof(TClient)]);
    }
}
