namespace Mitrol.Framework.Domain.SignalR
{
    using Mitrol.Framework.Domain.SignalR.EventLog;

    /// <summary>
    /// SignalR client contract for receiving and sending EventLog's events.
    /// </summary>
    public interface IEventLogHubClient : IEventLogHub
    {
        // NOTE: the event log hub does not push any notification to its client.
        // It is used to receive and then pass the log-events to the storage services.
    }
}
