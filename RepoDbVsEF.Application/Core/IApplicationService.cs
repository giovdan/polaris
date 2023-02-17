namespace RepoDbVsEF.Application.Core
{
    using RepoDbVsEF.Domain.Interfaces;
    using System.Collections.Generic;

    public interface IApplicationService
    {
        void SetSession(IUserSession userSession);
    }
}
