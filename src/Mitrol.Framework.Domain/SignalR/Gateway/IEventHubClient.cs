namespace Mitrol.Framework.Domain.SignalR.Gateway
{
    using Mitrol.Framework.Domain.Bus.Events;
    using System;

    /// <summary>
    /// SignalR client contract for hubs responsible to publish or react to progress events.
    /// </summary>
    public interface IEventHubClient : IEventHub
    {
        /// <summary>
        /// Notifies the provider that a ProgressEvent notification is to receive.
        /// </summary>
        /// <param name="handler">The Action to execute receiving the notification subject.</param>
        /// <returns>A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them.</returns>
        IDisposable ProgressEvent(Action<ProgressEvent> handler);

        /// <summary>
        /// Notifies the provider that a StatusEvent notification is to receive.
        /// </summary>
        /// <param name="handler">The Action to execute receiving the notification subject.</param>
        /// <returns>A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them.</returns>
        IDisposable StatusEvent(Action<StatusEvent> handler);

        /// <summary>
        /// Notifies the provider that a ProcessingProgressChangedEvent notification is to receive.
        /// </summary>
        /// <param name="handler">The Action to execute receiving the notification subject.</param>
        /// <returns>A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them.</returns>
        IDisposable ProcessingProgressChangedEvent(Action<ProcessingProgressChangedEvent> handler);

        /// <summary>
        /// Notifies the provider that a CommandEvent notification is to receive.
        /// </summary>
        /// <param name="handler">The Action to execute receiving the notification subject.</param>
        /// <returns>A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them.</returns>
        IDisposable CommandEvent(Action<CommandEvent> handler);

        /// <summary>
        /// Notifies the provider that a SaveProgressChangedEvent notification is to receive.
        /// </summary>
        /// <param name="handler">The Action to execute receiving the notification subject.</param>
        /// <returns>A reference to an interface that allows observers to stop receiving notifications before the provider has finished sending them.</returns>
        IDisposable SaveProgressChangedEvent(Action<SaveProgressChangedEvent> handler);
    }
}