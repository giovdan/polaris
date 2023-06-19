namespace Mitrol.Framework.MachineManagement.Application.Interfaces
{
    using Mitrol.Framework.Domain.Core.Models.Microservices;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Enums;
    using Mitrol.Framework.MachineManagement.Application.Models;
    using Mitrol.Framework.MachineManagement.Application.Transformations;
    using System.Collections.Generic;

    public interface ISetup : IProcessingSetup
    {
        new GeneralSetup General { get; set; }

        // TODO
        //new IEnumerable<DrillHead> DrillHeads { get; }
        //new IEnumerable<PlaTorch> PlaTorches { get; }
        //new IEnumerable<OxyTorch> OxyTorches { get; }
        //new IEnumerable<Station> Stations { get; }
        //new IEnumerable<Saw> Saws { get; }

        SetupStateEnum Status { get; set; }
        SetupActionEnum ToolStatus { get; set; }
        // TODO
        //void UpdateStatus(UpdateSourceEnum updateSource, ISetupToolsHubClient setupToolsHubClient);
        bool Save();
        void ResetRequiredToolAndToolTable();
        SetupStatusInfo GetSetupStatus();
    }

    
}
