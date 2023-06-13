namespace Mitrol.Framework.Domain.SignalR.AuthServer
{
    using Mitrol.Framework.Domain.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// SignalR server contract that it is responsible to signal authentication-server's events.
    /// </summary>
    public interface IAuthenticationServerHub
    {
        /// <summary>
        /// Notifies registered clients when a new UserSession is created.
        /// </summary>
        /// <param name="userSession">The newly created UserSession object.</param>
        /// <returns>Task representing the asynchronous operation.</returns>
        Task UserSession(UserSession userSession);
    }
}