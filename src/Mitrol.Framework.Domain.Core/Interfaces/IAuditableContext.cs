namespace Mitrol.Framework.Domain.Core.Interfaces
{
    /// <summary>
    /// Interface for handling Db Audit Context
    /// </summary>
    public interface IAuditableContext
    {
        void ApplyTrackAuditLogics();
    }
}