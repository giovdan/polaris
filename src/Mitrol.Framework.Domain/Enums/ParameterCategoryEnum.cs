namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    public enum ParameterCategoryEnum : int
    {
        [DatabaseDisplayName("GEN")]
        [Description("Generale")]
        GEN = 1,
        
        [DatabaseDisplayName("BAR")]
        [Description("Barriera")]
        BAR = 2,

        [DatabaseDisplayName("TMP")]
        [Description("Tempo")]
        TMP = 4,

        [DatabaseDisplayName("PAR")]
        [Description("Generico")]
        PAR = 8,

        [DatabaseDisplayName("VAR")]
        [Description("Variabile")]
        VAR = 16,

        [DatabaseDisplayName("PAXE")]
        [Description("Parametro asse")]
        PAXE = 32
    }
}