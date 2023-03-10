namespace Mitrol.Framework.Gateway.Api.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using Mitrol.Framework.Domain.Bus.Events;
    //using Mitrol.Framework.Domain.SignalR;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Concrete SignalR server publishes or reacts to events.
    /// </summary>
    //public class EventHub : Hub<IEventHub>, IEventHub
    //{
    //    public override Task OnConnectedAsync()
    //    {
    //        return base.OnConnectedAsync();
    //    }

    //    public override Task OnDisconnectedAsync(Exception exception)
    //    {
    //        return base.OnDisconnectedAsync(exception);
    //    }

    //    /// <summary>
    //    /// Raises a CommandEvent event.
    //    /// </summary>
    //    /// <param name="commandEvent">The notification subject.</param>
    //    /// <returns>Task representing the asynchronous operation.</returns>
    //    public Task CommandEvent(CommandEvent commandEvent) => Clients.Others.CommandEvent(commandEvent);

    //    /// <summary>
    //    /// Raises a ProcessingProgressChangedEvent event.
    //    /// </summary>
    //    /// <param name="processingProgressChangedEvent">The notification subject.</param>
    //    /// <returns>Task representing the asynchronous operation.</returns>
    //    public Task ProcessingProgressChangedEvent(ProcessingProgressChangedEvent processingProgressChangedEvent) => Clients.All.ProcessingProgressChangedEvent(processingProgressChangedEvent);

    //    /// <summary>
    //    /// Raises a ProgressEvent event.
    //    /// </summary>
    //    /// <param name="progressEvent">The notification subject.</param>
    //    /// <returns>Task representing the asynchronous operation.</returns>
    //    public Task ProgressEvent(ProgressEvent progressEvent) => Clients.Others.ProgressEvent(progressEvent);

    //    /// <summary>
    //    /// Raises a SaveProgressChangedEvent event.
    //    /// </summary>
    //    /// <param name="saveProgressChangedEvent">The notification subject.</param>
    //    /// <returns>Task representing the asynchronous operation.</returns>
    //    public Task SaveProgressChangedEvent(SaveProgressChangedEvent saveProgressChangedEvent) => Clients.Others.SaveProgressChangedEvent(saveProgressChangedEvent);

    //    /// <summary>
    //    /// Raises a StatusEvent event.
    //    /// </summary>
    //    /// <param name="statusEvent">The notification subject.</param>
    //    /// <returns>Task representing the asynchronous operation.</returns>
    //    public Task StatusEvent(StatusEvent statusEvent) => Clients.Others.StatusEvent(statusEvent);
    //}
}
