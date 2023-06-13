namespace Mitrol.Framework.Domain.SignalR
{
    /// <summary>
    /// SignalR client contract for receiving and sending EventLog's events.
    /// </summary>
    public interface INotesHubClient : INotesHub
    {
        // NOTE: the note server hub does not push any notification to its client.
        // It is used to receive and then pass the log-events to the storage services.
    }
}
