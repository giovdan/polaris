namespace Mitrol.Framework.Domain.Interfaces
{
    using System;

    public interface IDatabaseContext : IDisposable
    {
        void SetSession(IUserSession session);

    }
}
