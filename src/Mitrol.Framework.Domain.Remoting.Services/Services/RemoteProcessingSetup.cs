namespace Mitrol.Framework.Domain.Remoting.Services
{
    using Mitrol.Framework.Domain.Models;
    using Mitrol.Framework.Domain.Interfaces;
    using System.Collections.Generic;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using Mitrol.Framework.Domain.Remoting.Services.WebApi;
    using Mitrol.Framework.Domain.Configuration;

    public class RemoteProcessingSetup : IProcessingSetup
    {
        public WebApiCaller WebApiCaller { get; }

        public IUserSession UserSession { get; }

        public RemoteProcessingSetup(WebApiRestClient remoteData)
        {
            WebApiCaller = remoteData.WebApiCaller;
            UserSession = remoteData.UserSession;
        }

        private IProcessingSetup Setup
        {
            get
            {
                if (_remoteSetup == null)
                {
                    //{{url}}/api/v1/machineSetup
                    var request = new WebApiRequest(UserSession)
                    {
                        Uri = "machineSetup",
                        IsOldStyle = false
                    };

                    WebApiCaller.Get<SetupItem>(request)
                        .OnSuccess(setup => _remoteSetup = setup);
                }
                return _remoteSetup;
            }
        }
        private IProcessingSetup _remoteSetup;

        public IReadOnlyGeneralSetup General => Setup.General;
        IEnumerable<IReadOnlyDrillHead> IReadOnlySetup.DrillHeads => Setup.DrillHeads;
        IEnumerable<IReadOnlyPlaTorch> IReadOnlySetup.PlaTorches => Setup.PlaTorches;
        IEnumerable<IReadOnlyOxyTorch> IReadOnlySetup.OxyTorches => Setup.OxyTorches;
        public IEnumerable<IReadOnlyStation> Stations => Setup.Stations;


        ICollection<RequiredTool> IProcessingSetup.RequiredToolManagementIds => Setup.RequiredToolManagementIds;
        ICollection<RequiredToolTable> IProcessingSetup.RequiredToolTables => Setup.RequiredToolTables;

        public IEnumerable<IReadOnlySaw> Saws => Setup.Saws;
    }
    public class SetupItem : IProcessingSetup
    {
        public RemoteGeneralSetup General { get; set; }
        public IEnumerable<RemoteDrillHead> DrillHeads { get; set; }

        public IEnumerable<RemotePlaTorch> PlaTorches { get; set; }
        public IEnumerable<RemoteOxyTorch> OxyTorches { get; set; }
        public IEnumerable<RemoteStation> Stations { get; set; }
        public IEnumerable<RemoteSaw> Saws { get; set; }

        public ICollection<RequiredTool> RequiredToolManagementIds { get; set; }

        public ICollection<RequiredToolTable> RequiredToolTables { get; set; }

        IReadOnlyGeneralSetup IReadOnlySetup.General => General;
        IEnumerable<IReadOnlyDrillHead> IReadOnlySetup.DrillHeads => DrillHeads;
        IEnumerable<IReadOnlyPlaTorch> IReadOnlySetup.PlaTorches => PlaTorches;
        IEnumerable<IReadOnlyOxyTorch> IReadOnlySetup.OxyTorches => OxyTorches;
        IEnumerable<IReadOnlyStation> IReadOnlySetup.Stations => Stations;

        IEnumerable<IReadOnlySaw> IReadOnlySetup.Saws => Saws;
    }

    public class RemoteSaw : IReadOnlySaw
    {
        [JsonProperty("GridPosition")]
        public GridPosition GridPosition { get; set; }
        /// <summary>
        /// Tipo di segatrice (da configurazione)
        /// </summary>
        [JsonProperty("Type")]
        public SawingMachineTypeEnum Type { get; set; }

        /// <summary>
        /// Tipo di lama (da configurazione)
        /// </summary>
        [JsonProperty("BladeType")]
        public BladeTypeEnum BladeType { get; set; }

        /// <summary>
        /// Tipologia di velocità di avanzamento (da configurazione)
        /// </summary>
        [JsonProperty("BladeForwardSpeedType")]
        public BladeForwardSpeedTypeEnum BladeForwardSpeedType { get; set; }

        /// <summary>
        /// Indice tabella segatrice
        /// </summary>
        [JsonProperty("TableId")]
        public ushort TableId { get; set; }

        [JsonProperty("Unit")]
        public UnitEnum Unit { get; set; }

        [JsonProperty("ToolIndex")]
        public int CurrentToolId { get; set; }
    }

    public class RemoteGeneralSetup : IReadOnlyGeneralSetup
    {
        [JsonProperty("ANG")]
        public float Angle { get; set; }
        [JsonProperty("BENDED")]
        public bool Bended { get; set; }
        [JsonProperty("DSA")]
        public float Dsa { get; set; }
        [JsonProperty("DSB")]
        public float Dsb { get; set; }
        [JsonProperty("LB")]
        public float Lb { get; set; }
        [JsonProperty("LBR")]
        public float Lbr { get; set; }
        [JsonProperty("LBRI")]
        public float Lbri { get; set; }
        [JsonProperty("WL")]
        public float LinearWeight { get; set; }
        [JsonProperty("MaterialName")]
        public string MaterialCode { get; set; }
        [JsonProperty("MaterialType")]
        public MaterialTypeEnum MaterialType { get; set; }
        [JsonProperty("OC")]
        public float Oc { get; set; }
        [JsonProperty("OCX")]
        public float Ocx { get; set; }
        [JsonProperty("OFFPZ")]
        public float OffPz { get; set; }
        [JsonProperty("OP")]
        public float Op { get; set; }
        [JsonProperty("PL")]
        public float Pl { get; set; }
        [JsonProperty("CA")]
        public float PosCa { get; set; }
        [JsonProperty("CB")]
        public float PosCb { get; set; }
        [JsonProperty("CC")]
        public float PosCc { get; set; }
        [JsonProperty("PZA")]
        public float PosPzA { get; set; }
        [JsonProperty("PZB")]
        public float PosPzB { get; set; }
        [JsonProperty("PZC")]
        public float PosPzC { get; set; }
        [JsonProperty("ProfileName")]
        public string ProfileCode { get; set; }
        [JsonProperty("ProfileType")]
        public ProfileTypeEnum ProfileType { get; set; }
        [JsonProperty("PU")]
        public bool Pu { get; set; }
        [JsonProperty("R")]
        public float Radius { get; set; }
        [JsonProperty("RBF")]
        public float Rbf { get; set; }
        [JsonProperty("RF")]
        public float Rf { get; set; }
        [JsonProperty("RI")]
        public float Ri { get; set; }
        [JsonProperty("SA")]
        public float Sa { get; set; }
        [JsonProperty("SAB")]
        public float Sab { get; set; }
        [JsonProperty("SBA")]
        public float Sba { get; set; }
        [JsonProperty("SBB")]
        public float Sbb { get; set; }
        [JsonProperty("SEZ")]
        public float Section { get; set; }
        [JsonProperty("SW")]
        public float SpecificWeight { get; set; }
        [JsonProperty("TA")]
        public float Ta { get; set; }
        [JsonProperty("TBA")]
        public float Tba { get; set; }
        [JsonProperty("TBB")]
        public float Tbb { get; set; }
    }

    public class RemoteDrillHead : IReadOnlyDrillHead
    {
        [JsonProperty("SpindlesOverride")]
        public short SpindlesOverride { get; set; }
        [JsonProperty("Slots")]
        public IEnumerable<RemoteHeadSlot> Slots { get; set; }
        [JsonProperty("ManualSlot")]
        RemoteHeadSlot ManualSlot { get; set; }
        [JsonProperty("SlotInSpindle")]
        public short SlotInSpindle { get; set; }
        [JsonProperty("Unit")]
        public UnitEnum Unit { get; set; }

        IReadOnlyHeadSlot IReadOnlyDrillHead.ManualSlot => ManualSlot;

        IEnumerable<IReadOnlyHeadSlot> IReadOnlyDrillHead.Slots => Slots;
    }

    public class RemoteHeadSlot : IReadOnlyHeadSlot
    {
        [JsonProperty("CurrentCoolingDownTime")]
        public ushort CurrentCoolingDownTime { get; set; }
        [JsonProperty("MaxLength")]
        public float MaxLength { get; set; }
        [JsonProperty("IsEnabled")]
        public bool IsEnabled { get; set; }
        [JsonProperty("IsAdditional")]
        public bool IsAdditional { get; set; }
        [JsonProperty("SlotPosition")]
        public short SlotPosition { get; set; }
        [JsonProperty("ToolIndex")]
        public int CurrentToolId { get; set; }
        [JsonProperty("WorkType")]
        public int CurrentWorkType { get; set; }
    }

    public class RemotePlaTorch : IReadOnlyPlaTorch
    {
        public float ArchVoltage { get; set; }
        public decimal Current { get; set; }

        [JsonProperty("DTorchPla")]
        public float DTorchPla { get; set; }
        [JsonProperty("Electrode")]
        public string Electrode { get; set; }
        public int MixGas1 { get; set; }
        public int MixGas2 { get; set; }
        [JsonProperty("Nozzle")]
        public string Nozzle { get; set; }
        [JsonProperty("NozzleRetCap")]
        public string NozzleRetCap { get; set; }
        public byte NumTorce { get; set; }
        public short Path { get; set; }
        public int PlasmaCutflowPressure { get; set; }
        public GasTypeXprEnum PlasmaGas { get; set; }
        public int PlasmaPiercePressure { get; set; }
        public int ProcessID { get; set; }
        [JsonProperty("Shield")]
        public string Shield { get; set; }
        public int ShieldCutflowPressure { get; set; }
        public GasTypeXprEnum ShieldGas { get; set; }
        public int ShieldPiercePressure { get; set; }
        [JsonProperty("ShieldRetCap")]
        public string ShieldRetCap { get; set; }
        [JsonProperty("SwirlRing")]
        public string SwirlRing { get; set; }
        [JsonProperty("TableIdBevel")]
        public long TableIdBevel { get; set; }
        [JsonProperty("TableIdMark")]
        public long TableIdMark { get; set; }
        [JsonProperty("TableIdTrueHole")]
        public long TableIdTrueHole { get; set; }
        [JsonProperty("Thickness")]
        public float Thickness { get; set; }
        [JsonProperty("ToolIndex")]
        public int CurrentToolId { get; set; }
        public TorchTypeEnum TorchType { get; set; }
        [JsonProperty("Watertube")]
        public string Watertube { get; set; }
        public UnitEnum Unit { get; set; }
    }

    public class RemoteOxyTorch : IReadOnlyOxyTorch
    {
        public float AngleOxyBevelBottom { get; set; }
        public float AngleOxyBevelTop { get; set; }
        public float DTorchOxy1 { get; set; }
        public float DTorchOxy2 { get; set; }
        public float DTorchOxy3 { get; set; }
        public float HeightOxyBevelBottom { get; set; }
        public float HeightOxyBevelTop { get; set; }
        public float LastLifeTu { get; set; }
        public byte NumTorce { get; set; }
        public short Path { get; set; }
        public decimal Pco { get; set; }
        public decimal Pg { get; set; }
        public decimal Ppg { get; set; }
        public decimal Ppo { get; set; }
        public decimal Ppoa { get; set; }
        public long TableIdBevel { get; set; }
        [JsonProperty("ToolIndex")]
        public int CurrentToolId { get; set; }
        public TorchTypeEnum TorchType { get; set; }
        public UnitEnum Unit { get; set; }
    }

    public class RemoteStation : IReadOnlyStation
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("IsOriginValid")]
        public bool IsOriginValid { get; set; }
        [JsonProperty("Angle")]
        public double NA { get; set; }
        [JsonProperty("X")]
        public double NX { get; set; }
        [JsonProperty("Y")]
        public double NY { get; set; }
    }

}
