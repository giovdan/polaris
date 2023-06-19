namespace Mitrol.Framework.Domain.Enums
{
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]

    public enum MarkingUnitTypeEnum
    {
        [EnumSerializationName("NOTSPECIFIED")]
        NotSpecified=0,

        // Marcatura con unità mk a getto d'inchiostro senza rotazione testina (ugelli fissi)
        [EnumSerializationName("REAJET_F")]
        Reajet_F,

        // Marcatura con unità mk a getto d'inchiostro con rotazione testina
        [EnumSerializationName("REAJET")]
        Reajet,

        // Marcatura con unità plasma
        [EnumSerializationName("PLASMA")]
        Plasma,

        // Marcatura con unità DRILL		
        [EnumSerializationName("DRILL")]
        Drill
    }
}
