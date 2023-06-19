namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Handlers.Models;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Interfaccia per gestione PLC
    /// </summary>
    public interface IPLCApiService
    {
        /// <summary>
        /// Dizionario delle funzioni Plc/Cnc
        /// </summary>
        Dictionary<string, Task> PLC_Cycles { get; set; }
        Result StartPLCCycle(PLCCycleInfo parameters);
        Result UpdateToolOnCnc(ToolDetailItem toolUpdate);
    }
}
