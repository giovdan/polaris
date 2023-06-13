namespace Mitrol.Framework.Domain.SignalR.EventLog
{
    using Mitrol.Framework.Domain.Bus.Events;
    using System.Threading.Tasks;

    /// <summary>
    /// SignalR server contract for receiving and sending EventLog's events.
    /// </summary>
    public interface IEventLogHub
    {
        /// <summary>
        /// Passes down the specified diagnostic event to its storage service.
        /// </summary>
        /// <param name="diagnosticEvent">Diagnostic event to store.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task DiagnosticEvent(DiagnosticEvent diagnosticEvent);

        /// <summary>
        /// Passes down the specified notification event to its storage service.
        /// </summary>
        /// <param name="notificationEvent">Notification event to store.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task NotificationEvent(NotificationEvent notificationEvent);

        /// <summary>
        /// Passes down the specified work-done event to its storage service.
        /// </summary>
        /// <param name="workDoneEvent">Work-done event to store.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task ProductionWorkDoneEvent(ProductionWorkDoneEvent workDoneEvent);

        /// <summary>
        /// Passes down the specified log event to its storage service.
        /// </summary>
        /// <param name="logEvent">Log event to store.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task WriteLogEvent(WriteLogEvent logEvent);
    }
}
