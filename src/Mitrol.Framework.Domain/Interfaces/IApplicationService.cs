namespace Mitrol.Framework.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using System.Collections.Generic;


    /// <summary>
    /// Application Service Interface for Mitrol Core Application
    /// </summary>
    public interface IApplicationService
    {
        void SetSession(IUserSession userSession);
    }    
}