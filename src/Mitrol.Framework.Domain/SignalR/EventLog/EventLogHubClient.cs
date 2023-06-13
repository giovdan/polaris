namespace Mitrol.Framework.Domain.SignalR.EventLog
{
    using Microsoft.AspNetCore.SignalR.Client;
    using Mitrol.Framework.Domain.Bus.Events;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Concrete SignalR client implementation that publishes or reacts EventLog's events.
    /// </summary>
    public class EventLogHubClient : HubClient, IEventLogHubClient
    {
        /// <summary>
        /// The URL used to contact to the server.
        /// </summary>
        protected override string HubUrl => $"{Environment.GetEnvironmentVariable("MITROLWEBAPI_GATEWAYURL") ?? "http://localhost:4000"}/eventLog/events";

        /// <summary>
        /// Raises a DiagnosticEvent event.
        /// </summary>
        /// <param name="diagnosticEvent">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public Task DiagnosticEvent(DiagnosticEvent diagnosticEvent) => HubConnection.InvokeAsync(nameof(IEventLogHub.DiagnosticEvent), diagnosticEvent);

        /// <summary>
        /// Raises a NotificationEvent event.
        /// </summary>
        /// <param name="notificationEvent">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public Task NotificationEvent(NotificationEvent notificationEvent) => HubConnection.InvokeAsync(nameof(IEventLogHub.NotificationEvent), notificationEvent);

        /// <summary>
        /// Raises a ProductionWorkDoneEvent event.
        /// </summary>
        /// <param name="workDoneEvent">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public Task ProductionWorkDoneEvent(ProductionWorkDoneEvent workDoneEvent) => HubConnection.InvokeAsync(nameof(IEventLogHub.ProductionWorkDoneEvent), workDoneEvent);

        /// <summary>
        /// Raises a WriteLogEvent event.
        /// </summary>
        /// <param name="logEvent">The notification subject.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        public Task WriteLogEvent(WriteLogEvent logEvent) => HubConnection.InvokeAsync(nameof(IEventLogHub.WriteLogEvent), logEvent);
    }
}
