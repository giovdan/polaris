namespace Mitrol.Framework.Domain.Core.Interfaces
{
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;

    /// <summary>
    /// Interface for implementing BootableService
    /// </summary>
    public interface IBootableService
    {
        Result Boot(IUserSession userSession);
        Result CleanUpBeforeBoot(IUserSession userSession);
    }
}
