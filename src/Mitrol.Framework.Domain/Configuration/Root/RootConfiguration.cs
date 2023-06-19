namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Configuration.License;
    using Newtonsoft.Json;

    /// <summary>
    /// Class that represents the whole set of Machine configuration settings
    /// </summary>
    public class RootConfiguration : IRootConfiguration
    {
        internal const string s_backLogsJsonProperty = "Backlogs";
        internal const string s_backupJsonProperty = "Backup";
        internal const string s_cleanUpJsonProperty = "Cleanup";
        internal const string s_cncJsonProperty = "Cnc";
        internal const string s_defaultCultureJsonName = "DefaultCulture";
        internal const string s_importExportJsonProperty = "ImportExport";
        internal const string s_machineJsonProperty = "Machine";
        internal const string s_nameJsonName = "Name";
        internal const string s_plantJsonProperty = "Plant";
        internal const string s_plcJsonProperty = "Plc";
        internal const string s_productionJsonProperty = "Production";
        internal const string s_programmingJsonProperty = "Programming";
        internal const string s_serialNumberJsonName = "SerialNumber";
        internal const string s_setupJsonProperty = "Setup";
        internal const string s_licenseJsonProperty = "License";

        /// <summary>
        /// Default constructor required for AutoMapper mappings.
        /// </summary>
        public RootConfiguration()
        { }

        /// <summary>
        /// Json constructor that allows de-serialization of read-only properties
        /// </summary>
        [JsonConstructor]
        public RootConfiguration([JsonProperty(s_defaultCultureJsonName)] string defaultCulture,
                                 [JsonProperty(s_nameJsonName)] string name,
                                 [JsonProperty(s_serialNumberJsonName)] string serialNumber,
                                 [JsonProperty(s_plantJsonProperty)] PlantConfiguration plant,
                                 [JsonProperty(s_machineJsonProperty)] MachineConfiguration machine,
                                 [JsonProperty(s_plcJsonProperty)] PlcConfiguration plc,
                                 [JsonProperty(s_cncJsonProperty)] CncConfiguration cnc,
                                 [JsonProperty(s_programmingJsonProperty)] ProgrammingConfiguration program,
                                 [JsonProperty(s_backLogsJsonProperty)] BacklogsConfiguration backlogs,
                                 [JsonProperty(s_cleanUpJsonProperty)] CleanUpConfiguration cleanUp,
                                 [JsonProperty(s_setupJsonProperty)] SetupConfiguration setup,
                                 [JsonProperty(s_importExportJsonProperty)] ImportExportConfiguration importExport,
                                 [JsonProperty(s_productionJsonProperty)] ProductionConfiguration production,
                                 [JsonProperty(s_backupJsonProperty)] BackupConfiguration backup,
                                 [JsonProperty(s_licenseJsonProperty)] LicenseConfiguration license)
        {
            Plant = plant;
            Machine = machine;
            Plc = plc;
            Cnc = cnc;
            ImportExport = importExport;
            Production = production;
            Programming = program;
            Backlogs = backlogs;
            CleanUp = cleanUp;
            Setup = setup;
            DefaultCulture = defaultCulture;
            Name = name;
            SerialNumber = serialNumber;
            Backup = backup;
            License = license;
        }

        [JsonProperty(s_backLogsJsonProperty)]
        public BacklogsConfiguration Backlogs { get; protected set; }

        [JsonProperty(s_backupJsonProperty)]
        public BackupConfiguration Backup { get; protected set; }

        [JsonProperty(s_cleanUpJsonProperty)]
        public CleanUpConfiguration CleanUp { get; protected set; }

        [JsonProperty(s_cncJsonProperty)]
        public CncConfiguration Cnc { get; protected set; }

        [JsonProperty(s_defaultCultureJsonName)]
        public string DefaultCulture { get; protected set; }

        [JsonProperty(s_importExportJsonProperty)]
        public ImportExportConfiguration ImportExport { get; protected set; }

        [JsonProperty(s_machineJsonProperty)]
        public MachineConfiguration Machine { get; protected set; }

        [JsonProperty(s_nameJsonName)]
        public string Name { get; protected set; }

        [JsonProperty(s_plantJsonProperty)]
        public PlantConfiguration Plant { get; protected set; }

        [JsonProperty(s_plcJsonProperty)]
        public PlcConfiguration Plc { get; protected set; }

        [JsonProperty(s_productionJsonProperty)]
        public ProductionConfiguration Production { get; protected set; }

        [JsonProperty(s_programmingJsonProperty)]
        public ProgrammingConfiguration Programming { get; protected set; }

        [JsonProperty(s_serialNumberJsonName)]
        public string SerialNumber { get; protected set; }
        
        [JsonProperty(s_setupJsonProperty)]
        public SetupConfiguration Setup { get; protected set; }

        [JsonProperty(s_licenseJsonProperty)]
        public LicenseConfiguration License { get; protected set; }
    }
}
