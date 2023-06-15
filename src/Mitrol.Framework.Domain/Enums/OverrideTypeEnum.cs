namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    [DefaultValue("DeltaValue")]
    public enum OverrideTypeEnum
    {
        [EnumSerializationName("None")]
        [DatabaseDisplayName("None")]
        [Description("Nessuna tipologia")]
        None = 1,

        [EnumSerializationName("Absolute")]
        [DatabaseDisplayName("DeltaValue")]
        [Description("Valore relativo (Delta)")]
        DeltaValue=2,

        [EnumSerializationName("Percentage")]
        [DatabaseDisplayName("DeltaPercentage")]
        [Description("Percentuale relativa (Delta)")]
        DeltaPercentage = 4
    }
}