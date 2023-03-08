namespace Mitrol.Framework.Domain.Core.Interfaces
{
    using Mitrol.Framework.Domain.Interfaces;


    /// <summary>
    /// Application Service Interface for Mitrol Core Application
    /// </summary>
    public interface IApplicationService
    {
        void SetSession(IUserSession userSession);
    }    
}