namespace Mitrol.Framework.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Models;
    using System.Collections.Generic;

    /// <summary>
    /// Setup interface for processing
    /// </summary>
    public interface IProcessingSetup : IReadOnlySetup
    {
        ICollection<RequiredTool> RequiredToolManagementIds { get; }
        ICollection<RequiredToolTable> RequiredToolTables { get; }
    }
}