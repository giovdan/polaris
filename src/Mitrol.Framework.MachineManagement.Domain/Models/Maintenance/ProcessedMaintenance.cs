
namespace Mitrol.Framework.MachineManagement.Domain.Models
{
    using Mitrol.Framework.MachineManagement.Domain.Enums;
    using System;

    /// <summary>
    /// View For Processed Maintenance
    /// </summary>
    public class ProcessedMaintenance
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public MaintenanceCategoryEnum Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public MaintenanceOwnerEnum Owner { get; set; }
        public MaintenanceStatusEnum Status { get; set; }
        public MaintenanceTypeEnum Type { get; set; }
        public long TimeSpentOn { get; set; }
        public DateTime LastExecutionDate { get; set; }
        public int NumberOfMaintenances { get; set; }
        public long ExpirationTime { get; set; }
        public long UntilTime { get; set; }
        public int GivenNoticeMode { get; set; }
        public long EstimatedTime { get; set; }
        public long RepeatEach { get; set; }
        public long CurrentWorkingTime { get; set; }
        public bool IsMandatory { get; set; }
    }
}
