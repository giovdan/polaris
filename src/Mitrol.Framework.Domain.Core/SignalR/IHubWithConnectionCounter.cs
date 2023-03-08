namespace Mitrol.Framework.Domain.Core.SignalR
{
    using System.Threading.Tasks;

    /// <summary>
    /// SignalR server contract that it is responsible to notify the number of established connections.
    /// </summary>
    public interface IHubWithConnectionCounter
    {
        /// <summary>
        /// Notify to all clients the new value of the connection counter.
        /// </summary>
        /// <param name="onlineClientsCount">The number of established connections.</param>
        /// <returns>A System.Threading.Tasks.Task that represents the asynchronous operation.</returns>
        Task ConnectedClientCount(int onlineClientsCount);
    }
}
