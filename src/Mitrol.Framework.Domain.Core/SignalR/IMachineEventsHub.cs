namespace Mitrol.Framework.Domain.Core.SignalR
{
    using Mitrol.Framework.Domain.Bus.Events;
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Core.Models.Microservices;
    using System.Net.NetworkInformation;
    using System.Threading.Tasks;

    /// <summary>
    /// SignalR server contract that it is responsible to signal machine events.
    /// </summary>
    public interface IMachineEventsHub : IHubWithConnectionCounter, IStationOriginsHub
    {
        /// <summary>
        /// Registered clients get notified when the active alarm changes.
        /// </summary>
        /// <param name="activeAlarm">The current active alarm.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task ActiveAlarm(MachineNotification activeAlarm);

        /// <summary>
        /// Registered clients get notified when the machine state changes.
        /// </summary>
        /// <param name="machineStatusInfo">The current machine state.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task MachineStatusInfo(MachineStatusInfo machineStatusInfo);

        /// <summary>
        /// Registered clients get notified when a new NotificationEvent arises.
        /// </summary>
        /// <param name="machineStatusInfo">The current machine state.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task NotificationEvent(NotificationEvent notificationEvent);

        /// <summary>
        /// Registered clients get notified when a the VPN connection status changes.
        /// </summary>
        /// <param name="status">The current operational status.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task VPNStatus(OperationalStatus status);
    }
}
