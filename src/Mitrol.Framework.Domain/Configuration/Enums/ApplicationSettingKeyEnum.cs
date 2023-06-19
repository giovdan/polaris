namespace Mitrol.Framework.Domain.Configuration.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    public enum ApplicationSettingKeyEnum : long
    {
        [DatabaseDisplayName("Version")]
        [Description("Versione dell'applicazione")]
        [DefaultValue("1.0.0")]
        Version = 1,

        [DatabaseDisplayName("ApplicationName")]
        [Description("Nome dell'applicazione")]
        [DefaultValue("Polaris")]
        ApplicationName = 2,

        [DatabaseDisplayName("ConfigurationFileName")]
        [Description("Nome del file di configuration")]
        [DefaultValue("MACHINE_CONFIGURATION.json")]
        ConfigurationFileName = 5,

        [DatabaseDisplayName("SetupFileName")]
        [Description("Nome del file di setup")]
        [DefaultValue("MACHINE_SETUP.json")]
        SetupFileName = 7,

        [DatabaseDisplayName("ConfigurationDirectory")]
        [Description("Nome della cartella di configurazione FICEP")]
        [DefaultValue("Config")]
        ConfigurationDirectory = 9,

        [DatabaseDisplayName("ObserverPollingInterval")]
        [Description("Intervallo di polling per la gestione della ritentività (in millisecondi)")]
        [DefaultValue("10000")]
        ObserverPollingInterval = 10,

        [DatabaseDisplayName("DataDirectory")]
        [Description("Nome della cartella data")]
        [DefaultValue("data")]
        DataDirectory = 11,

        [DatabaseDisplayName("PlcDirectory")]
        [Description("Nome della cartella plc")]
        [DefaultValue("plc")]
        PlcDirectory = 12,

        [DatabaseDisplayName("ExecutionFileName")]
        [Description("Nome del file dati di esecuzione")]
        [DefaultValue("MACHINE_EXECUTION.json")]
        ExecutionFileName = 14,

        [DatabaseDisplayName("ToastNotificationTimeout")]
        [Description("Scadenza di una notifica toast (in ms)")]
        [DefaultValue("3000")]
        ToastNotificationTimeout = 15,

        [DatabaseDisplayName("EniFileDirectory")]
        [Description("Nome della cartella Eni")]
        [DefaultValue("/D/Polaris/plc/")]
        EniFileDirectory = 16,

        [DatabaseDisplayName("MachineStatusFileName")]
        [Description("Nome del file dati di esecuzione")]
        [DefaultValue("MACHINE_STATUS.json")]
        MachineFileName = 18,

        [DatabaseDisplayName("DbVersion")]
        [Description("Versione Database")]
        [DefaultValue("1.0.0")]
        DbVersion = 19,

        [DatabaseDisplayName("KeyCounter")]
        [Description("Contatore del tempo di lavoro senza chiave")]
        [DefaultValue("0")]
        KeyCounter = 20,
    }
}
