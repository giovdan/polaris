namespace Mitrol.Framework.Domain.Core.Enums
{
    //using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Core.Attributes;
    using System.ComponentModel;

    public enum MachineStateEnum : int
    {
        // "UNDEFINED" : stato impianto non definito (non possibile)
        [Description("Stato non definito o non inizializzato")]
        [StatusCondition(StatusConditionEnum.Alarm)]
        Undefined = 0,

        // "Fault conf." : configurazione non valida (impianto non utilizzabile)
        [Description("Configurazione non valida")]
        [StatusCondition(StatusConditionEnum.Alarm)]
        ConfigurationFault = 1,

        // "Aux Off" : ausiliari spenti
        [Description("Ausiliari non attivi")]
        [StatusCondition(StatusConditionEnum.Alarm)]
        AuxiliaresOff = 2,

        // "Standby" : impianto in attesa di selezione dello stato automatico o manuale
        [Description("Standby")]
        [StatusCondition(StatusConditionEnum.Warning)]
        Standby = 4,

        // "Emergency" : pulsante EMERGENZA premuto
        [Description("Emergenza premuta")]
        [StatusCondition(StatusConditionEnum.Alarm)]
        Emergency = 8,

        // "Alarm" : allarme attivo
        [Description("Allarme in corso")]
        [StatusCondition(StatusConditionEnum.Alarm)]
        Alarm = 16,

        // "Message" : messaggio attivo
        [Description("Messaggio in corso")]
        [StatusCondition(StatusConditionEnum.Warning)]
        Message = 32,

        // "Setup" : impianto in setup
        [Description("Setup in corso")]
        [StatusCondition(StatusConditionEnum.Ready)]
        Setup = 64,

        // "Ready" : impianto pronto, in attesa di START
        [Description("In attesa di START")]
        [StatusCondition(StatusConditionEnum.Ready)]
        ReadyToStart = 128,

        // "Running" : impianto operativo in lavorazione
        [Description("In esecuzione")]
        [StatusCondition(StatusConditionEnum.Running)]
        Running = 256,

        // "Hold" : pulsante HOLD premuto
        [Description("HOLD in corso")]
        [StatusCondition(StatusConditionEnum.Warning)]
        OnHold = 512,

        // "Fault plc" : FAULT del programma plc
        [Description("Plc non Ok")]
        [StatusCondition(StatusConditionEnum.Alarm)]
        PlcNotOk = 1024,

        // "Reset" : pulsante RESET premuto o stato di RESET forzato da plc/cnc
        [Description("Reset")]
        [StatusCondition(StatusConditionEnum.Warning)]
        Reset = 2048,

        // "Manual" : impianto in manuale, es. per movimenti JOG
        [Description("Manual")]
        [StatusCondition(StatusConditionEnum.Ready)]
        Manual = 4096,

        // "Test" : attivo selettore impianto in modalità TEST
        [Description("Test")]
        [StatusCondition(StatusConditionEnum.Ready)]
        Test = 8192,
    }
}
