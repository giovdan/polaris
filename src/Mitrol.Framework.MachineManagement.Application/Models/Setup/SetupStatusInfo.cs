namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.MachineManagement.Application.Enums;
    using Mitrol.Framework.MachineManagement.Application.Transformations;

    public class SetupStatusInfo
    {
        public string StatusLocalizationKey { get; set; }
        public SetupStateEnum Status { get; set; }

        public bool StatusGeneral { get; set; }
        public bool StatusOrigins { get; set; }
        public bool StatusTools { get; set; }
        
    }
}
