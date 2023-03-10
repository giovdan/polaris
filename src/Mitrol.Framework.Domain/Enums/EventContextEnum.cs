namespace Mitrol.Framework.Domain.Enums
{
    using System.ComponentModel;

    public enum EventContextEnum : int
    {
        [Description("Contesto Errore Interno")]
        InternalServerError = 0,
        [Description("Contesto Users")]
        User = 1,
        [Description("Contesto Tools")]
        Tool = 2,
        [Description("Contesto tool tables")]
        ToolTable = 4,
        [Description("Contesto Paramters")]
        Parameter = 8,
        [Description("Contesto Configuration")]
        Configuration = 16,
        [Description("Contesto Profiles")]
        Profile = 32,
        [Description("Contesto Materials")]
        Material = 64,
        [Description("Contesto Machine")]
        Machine = 128,
        [Description("Contesto Alarms")]
        Alarm = 256,
        Setup = 1024,
        [Description("Contesto di Boot")]
        Boot = 2048,
        [Description("Contesto Esecuzione ISO")]
        ISOExecution = 4096,
        [Description("Contesto Esecuzione DNC")]
        DNC = 8192,
        [Description("Contesto PLC")]
        PLC = 16384,
        [Description("Contesto Note")]
        Notes = 32768,
        [Description("Contesto Interventi di Manutenzione")]
        Maintenances = Notes * 2,
        [Description("Tabelle Bevel e/o TrueHole")]
        ToolSubRange = Maintenances * 2
    }
}
