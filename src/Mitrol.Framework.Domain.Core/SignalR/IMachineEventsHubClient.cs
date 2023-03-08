namespace Mitrol.Framework.Domain.Core.SignalR
{
    /// <summary>
    /// SignalR client contract that receives machine events.
    /// </summary>
    public interface IMachineEventsHubClient : IMachineEventsHub
    {
        /// <summary>
        /// True if there is any established connection but the requester; otherwise false.
        /// </summary>
        bool AnyClientOnline { get; }
    }
}
