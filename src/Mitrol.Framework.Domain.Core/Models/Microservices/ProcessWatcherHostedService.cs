namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Microsoft.Extensions.DependencyInjection;
    using Mitrol.Framework.Domain.Core.Interfaces;
    using Mitrol.Framework.Domain.Core.SignalR;
    using System;
    using System.Management;
    using System.Runtime.Versioning;
    using System.Threading.Tasks;

    [SupportedOSPlatform("windows")]
    public class ProcessWatcher : BackgroundTaskQueue, IProcessWatcher
    {
        private const string s_openVPNProcessName = "openvpn.exe";
        private const string s_property_TargetInstance = "TargetInstance";
        private const string s_property_Name = "Name";

        public ManagementEventWatcher CreateProcessEventWatcher { get; set; }
        public ManagementEventWatcher DeleteProcessEventWatcher { get; set; }

        public ProcessWatcher()
        {
            // handle events
            if (OperatingSystem.IsWindows())
            {
                CreateProcessEventWatcher = new ManagementEventWatcher("SELECT * FROM __InstanceCreationEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'");
                CreateProcessEventWatcher.EventArrived += CreateProcessEventHandler;
                CreateProcessEventWatcher.Start();
                DeleteProcessEventWatcher = new ManagementEventWatcher("SELECT * FROM __InstanceDeletionEvent WITHIN 1 WHERE TargetInstance ISA 'Win32_Process'");
                DeleteProcessEventWatcher.EventArrived += DeleteProcessEventHandler;
                DeleteProcessEventWatcher.Start();
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }

        private void CreateProcessEventHandler(object sender, EventArrivedEventArgs e)
        {
            QueueWorkItem((scope, token) =>
            {
                var processName = ((ManagementBaseObject)e.NewEvent[s_property_TargetInstance])[s_property_Name]?.ToString();

                if (string.Compare(processName, s_openVPNProcessName, ignoreCase: true) == 0)
                {
                    var machineEventHubClient = scope.ServiceProvider.GetRequiredService<IMachineEventsHubClient>();
                    return machineEventHubClient.VPNStatus(System.Net.NetworkInformation.OperationalStatus.Up);
                }
                else
                {
                    return Task.CompletedTask;
                }
            });
        }

        private void DeleteProcessEventHandler(object sender, EventArrivedEventArgs e)
        {
            QueueWorkItem((scope, token) =>
            {
                var processName = ((ManagementBaseObject)e.NewEvent[s_property_TargetInstance])[s_property_Name]?.ToString();

                if (string.Compare(processName, s_openVPNProcessName, ignoreCase: true) == 0)
                {
                    var machineEventHubClient = scope.ServiceProvider.GetRequiredService<IMachineEventsHubClient>();
                    return machineEventHubClient.VPNStatus(System.Net.NetworkInformation.OperationalStatus.Down);
                }
                else
                {
                    return Task.CompletedTask;
                }
            });
        }

        protected override void DisposeManaged()
        {
            base.DisposeManaged();
            CreateProcessEventWatcher?.Dispose();
            DeleteProcessEventWatcher?.Dispose();
        }
    }

    public class ProcessWatcherHostedService : BackgroundTasksHostedService
    {
        public ProcessWatcherHostedService(IProcessWatcher processWatcher, IServiceScopeFactory serviceScopeFactory)
            : base(processWatcher, serviceScopeFactory) { }
    }
}
