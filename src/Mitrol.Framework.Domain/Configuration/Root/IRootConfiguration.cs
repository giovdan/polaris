using Mitrol.Framework.Domain.Configuration.License;
using System.Collections.Generic;

namespace Mitrol.Framework.Domain.Configuration
{
    public interface IRootConfiguration
    {
        string DefaultCulture { get; }
        string Name { get; }
        string SerialNumber { get; }
        BacklogsConfiguration Backlogs { get; }
        CleanUpConfiguration CleanUp { get; }
        CncConfiguration Cnc { get; }
        MachineConfiguration Machine { get; }
        SetupConfiguration Setup { get; }
        PlantConfiguration Plant { get; }
        PlcConfiguration Plc { get; }
        ProgrammingConfiguration Programming { get; }
        ImportExportConfiguration ImportExport { get; }
        ProductionConfiguration Production { get; }
        BackupConfiguration Backup { get; }
        LicenseConfiguration License { get; }
    }
}
