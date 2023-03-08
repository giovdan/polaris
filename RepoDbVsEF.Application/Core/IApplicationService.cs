namespace Mitrol.Framework.MachineManagement.Application.Core
{
    using Mitrol.Framework.Domain.Interfaces;
    public interface IApplicationService
    {
        void SetSession(IUserSession userSession);
    }
}
