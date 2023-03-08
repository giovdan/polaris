namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Core.Attributes;
    using Mitrol.Framework.Domain.Core.Enums;
    using Newtonsoft.Json;

    public class MachineStatusInfo
    {
        public MachineStatusInfo(MachineStateEnum machineState, bool isoProgramEnabled)
        {
            MachineState = machineState;
            IsoOperationsEnabled = isoProgramEnabled;
            ProcessingEnabled = isoProgramEnabled;
        }

        [JsonProperty("IsoOperationsEnabled")]
        public bool IsoOperationsEnabled { get; set; }

        [JsonProperty("ProcessingEnabled")]
        public bool ProcessingEnabled { get; set; }

        [JsonProperty("MachineStatus")]
        public MachineStateEnum MachineState { get; set; }

        [JsonProperty("LocalizationKey")]
        public string MachineStatusLocalizationKey => $"LBL_MACHINESTATUS_{MachineState.ToString().ToUpper()}";

        [JsonProperty("StatusCondition")]
        public StatusConditionEnum StatusCondition
        {
            get
            {
                var statusColor = DomainExtensions.GetEnumAttribute<MachineStateEnum, StatusConditionAttribute>(MachineState);
                return statusColor?.StatusCondition ?? StatusConditionEnum.Inactive;
            }
        }
    }
}
