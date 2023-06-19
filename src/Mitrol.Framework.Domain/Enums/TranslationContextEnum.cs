namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    [Flags]
    public enum TranslationContextEnum : int
    {
        [DatabaseDisplayName("LBL")]
        [Description("Labels")]
        Labels = 1,

        [DatabaseDisplayName("ALR")]
        [Description("Alarms")]
        Alarms = 2,

        [DatabaseDisplayName("MSG")]
        [Description("Messaggi")]
        Messages = 4,

        [DatabaseDisplayName("INF")]
        [Description("Informations")]
        Informations = 8,

        [DatabaseDisplayName("PAR")]
        [Description("Parameters")]
        [RegularExpression("PAR[0-9]*$")]
        Parameters = 16,

        [DatabaseDisplayName("WAIT")]
        [Description("Attes")]
        Wait = 32,

        [DatabaseDisplayName("ERR")]
        [Description("Errors")]
        Errors = 64,

        [DatabaseDisplayName("VMSG")]
        [Description("Validation Messages")]
        ValidationMessages = 128,

        [DatabaseDisplayName("MAINTENANCE")]
        [Description("Maintenances")]
        Maintenances = 256,

        [DatabaseDisplayName("ALL")]
        [Description("Every context")]
        All = Labels | Alarms | Messages | Informations | Parameters | Messages | ValidationMessages | Maintenances
    }

}
