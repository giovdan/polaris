namespace RepoDbVsEF.Domain.Interfaces
{
    using System;

    public interface IDatabaseContext : IDisposable
    {
        void SetSession(IUserSession session);

    }
}
