namespace Mitrol.Framework.MachineManagement.Application.Execution
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Core.Models;
    using Mitrol.Framework.Domain.Core.Models.Microservices;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.MachineManagement.Application.Interfaces;
    using Mitrol.Framework.MachineManagement.Application.Models.Setup;
    using Mitrol.Framework.MachineManagement.Application.Transformations;
    using Mitrol.Framework.MachineManagement.Domain.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Execution Handler interface
    /// </summary>
    /// 
    public interface IExecution
    {

    }

    // TODO
    //public interface IExecution :
    //    IParameterHandler, IMachineObserver, IDisposable, IPLCApiService,
    //    ICodeController, IJogService, IOriginsService, IPlcPanelsService
    //{
        //MachineStatusData MachineStatusData { get; }
        //ExecutionData ExecData { get; }
        //MachineNotification ActiveAlarm { get; }
        //MachineNotification ActiveCncMessage { get; }
        //MachineStatusInfo MachineStatusInfo { get; }
        //IEnumerable<MachineNotification> ActiveAlarms { get; }
        //IEnumerable<MachineNotification> ActiveOperatorMessages { get; }
        //IUserSession UserSession { get; set; }
        //ISetup Setup { get; set; }
        //Result Initialize();
        //Result StartExecution();
        //Result StopExecution();
        //Result UpdateSetup();
        //List<DrillTool> GetTools(DrillToolId toolId);
        //List<PlaTool> GetTools(PlaToolId toolId);
        //List<OxyTool> GetTools(OxyToolId toolId);
        //Result ResetToolLife(long toolId);
        //void AggiornoLinatt(int lineNumber);
        //Result SelectProgramCode(int lineNumber, byte stationNumber);
        //Result<PlcVersion> CncReadVersPlc();
        //Result<RtmcData> CncReadRtcm();
        //Dictionary<string, IAxis> Axes { get; set; }
        //GeneralSetupDetailItem GetGeneralSetup(MeasurementSystemEnum measurementSystemTo);
        //Result ConfirmGeneralSetup();
        //Result UpdateGeneralSetup(GeneralSetupDetailItem generalSetupDetailItem);
        //bool UpdateProgramInProgress(long ProgramId);
    //}
}
