namespace Mitrol.Framework.Domain.SignalR
{
    using Mitrol.Framework.Domain.Bus.Events;
    using System.Threading.Tasks;

    /// <summary>
    /// SignalR server contract for hubs responsible to gather and manage progress events.
    /// </summary>
    public interface IEventHub
    {
        /// <summary>
        /// Raises a CommandEvent event.
        /// </summary>
        /// <param name="commandEvent">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task CommandEvent(CommandEvent commandEvent);

        /// <summary>
        /// Raises a ProcessingProgressChangedEvent event.
        /// </summary>
        /// <param name="processingProgressChangedEvent">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task ProcessingProgressChangedEvent(ProcessingProgressChangedEvent processingProgressChangedEvent);

        /// <summary>
        /// Raises a ProgressEvent event.
        /// </summary>
        /// <param name="progressEvent">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task ProgressEvent(ProgressEvent progressEvent);

        /// <summary>
        /// Raises a SaveProgressChangedEvent event.
        /// </summary>
        /// <param name="saveProgressChangedEvent">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task SaveProgressChangedEvent(SaveProgressChangedEvent saveProgressChangedEvent);

        /// <summary>
        /// Raises a StatusEvent event.
        /// </summary>
        /// <param name="statusEvent">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task StatusEvent(StatusEvent statusEvent);



        Task ToastNotification(ToastNotification notification);
    }
}