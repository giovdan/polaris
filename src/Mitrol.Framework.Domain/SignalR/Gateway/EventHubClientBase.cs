namespace Mitrol.Framework.Domain.SignalR.Gateway
{
    using Microsoft.AspNetCore.SignalR.Client;
    using Mitrol.Framework.Domain.Bus.Events;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Concrete SignalR client implementation that publishes or reacts to events.
    /// </summary>
    public abstract class EventHubClientBase : HubClient, IEventHubClient
    {
        /// <summary>
        /// Default constructor that initializes the hub connection.
        /// </summary>
        public EventHubClientBase() : base() { }

        /// <summary>
        /// Default constructor that initializes the hub connection at the specified URL.
        /// </summary>
        /// <param name="hubUrl">A System.String representing the hub's URL.</param>
        public EventHubClientBase(string hubUrl, int connectDelay = 5000) : base(hubUrl, connectDelay) { }

        #region < Hooks >

        /// <summary>
        /// Notifies the provider that a CommandEvent notification is to receive.
        /// </summary>
        /// <param name="handler">The Action to execute receiving the notification subject.</param>
        /// <returns>
        /// A reference to an interface that allows observers to stop receiving notifications before
        /// the provider has finished sending them.
        /// </returns>
        public virtual IDisposable CommandEvent(Action<CommandEvent> handler) => HubConnection.On(nameof(IEventHub.CommandEvent), handler);

        /// <summary>
        /// Notifies the provider that a ProcessingProgressChangedEvent notification is to receive.
        /// </summary>
        /// <param name="handler">The Action to execute receiving the notification subject.</param>
        /// <returns>
        /// A reference to an interface that allows observers to stop receiving notifications before
        /// the provider has finished sending them.
        /// </returns>
        public virtual IDisposable ProcessingProgressChangedEvent(Action<ProcessingProgressChangedEvent> handler) => HubConnection.On(nameof(IEventHub.ProcessingProgressChangedEvent), handler);

        /// <summary>
        /// Notifies the provider that a ProgressEvent notification is to receive.
        /// </summary>
        /// <param name="handler">The Action to execute receiving the notification subject.</param>
        /// <returns>
        /// A reference to an interface that allows observers to stop receiving notifications before
        /// the provider has finished sending them.
        /// </returns>
        public virtual IDisposable ProgressEvent(Action<ProgressEvent> handler) => HubConnection?.On(nameof(IEventHub.ProgressEvent), handler);

        /// <summary>
        /// Notifies the provider that a SaveProgressChangedEvent notification is to receive.
        /// </summary>
        /// <param name="handler">The Action to execute receiving the notification subject.</param>
        /// <returns>
        /// A reference to an interface that allows observers to stop receiving notifications before
        /// the provider has finished sending them.
        /// </returns>
        public virtual IDisposable SaveProgressChangedEvent(Action<SaveProgressChangedEvent> handler) => HubConnection.On(nameof(IEventHub.SaveProgressChangedEvent), handler);

        /// <summary>
        /// Notifies the provider that a StatusEvent notification is to receive.
        /// </summary>
        /// <param name="handler">The Action to execute receiving the notification subject.</param>
        /// <returns>
        /// A reference to an interface that allows observers to stop receiving notifications before
        /// the provider has finished sending them.
        /// </returns>
        public virtual IDisposable StatusEvent(Action<StatusEvent> handler) => HubConnection.On(nameof(IEventHub.StatusEvent), handler);

        #endregion < Hooks >

        #region < Publish methods >

        /// <summary>
        /// Raises a CommandEvent event.
        /// </summary>
        /// <param name="commandEvent">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public Task CommandEvent(CommandEvent commandEvent) => HubConnection.InvokeAsync(nameof(IEventHub.CommandEvent), commandEvent);

        /// <summary>
        /// Raises a ProcessingProgressChangedEvent event.
        /// </summary>
        /// <param name="processingProgressChangedEvent">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public Task ProcessingProgressChangedEvent(ProcessingProgressChangedEvent processingProgressChangedEvent) => HubConnection.InvokeAsync(nameof(IEventHub.ProcessingProgressChangedEvent), processingProgressChangedEvent);

        /// <summary>
        /// Raises a ProgressEvent event.
        /// </summary>
        /// <param name="progressEvent">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public Task ProgressEvent(ProgressEvent progressEvent) => HubConnection.InvokeAsync(nameof(IEventHub.ProgressEvent), progressEvent);

        /// <summary>
        /// Raises a SaveProgressChangedEvent event.
        /// </summary>
        /// <param name="saveProgressChangedEvent">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public Task SaveProgressChangedEvent(SaveProgressChangedEvent saveProgressChangedEvent) => HubConnection.InvokeAsync(nameof(IEventHub.SaveProgressChangedEvent), saveProgressChangedEvent);

        /// <summary>
        /// Raises a StatusEvent event.
        /// </summary>
        /// <param name="statusEvent">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public Task StatusEvent(StatusEvent statusEvent) => HubConnection.InvokeAsync(nameof(IEventHub.StatusEvent), statusEvent);



        public Task ToastNotification(ToastNotification notification) => HubConnection.InvokeAsync(nameof(IEventHub.ToastNotification), notification);

        #endregion < Publish methods >
    }
}
