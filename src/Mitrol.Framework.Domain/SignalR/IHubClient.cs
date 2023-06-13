namespace Mitrol.Framework.Domain.SignalR
{
    using Microsoft.AspNetCore.SignalR.Client;
    using System;
    using System.Threading.Tasks;

    public interface IHubClient
    {
        HubConnection HubConnection { get; }
    }

    public static class IHubClientExtensions
    {
        public static IDisposable On(this IHubClient hubConnection, string methodName, Func<Task> handler) => hubConnection.HubConnection.On(methodName, handler);
    }
}