namespace Mitrol.Framework.Domain.SignalR.AuthServer
{
    using Mitrol.Framework.Domain.Models;
    using System;

    /// <summary>
    /// SignalR client contract that receives authentication-server's events.
    /// </summary>
    public interface IAuthenticationServerHubClient
    {
        /// <summary>
        /// Notifies the provider that a UserSession notification is to receive.
        /// </summary>
        /// <param name="handler">The Action to execute receiving the notification subject.</param>
        /// <returns>
        /// A reference to an interface that allows observers to stop receiving notifications before
        /// the provider has finished sending them.
        /// </returns>
        IDisposable UserSession(Action<UserSession> handler);
    }
}