using System.ComponentModel;

namespace Mitrol.Framework.Domain.Enums
{
    // TODO: cambiano in base ad un file di configurazione (mk con scribing/plasma/inkjet)
    // quindi questo file andrà poi eliminato

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum OperationFontTypeEnum : int
    {
        [EnumSerializationName("NotDefined")]
        NotDefined = 0,

        [EnumSerializationName("FNT05X05")]
        FNT05X05 = 5,

        [EnumSerializationName("FNT10x10")]
        FNT10X10 = 1,

        [EnumSerializationName("FNT16x16")]
        FNT16X16 = 6,

        [EnumSerializationName("FNT20x20")]
        FNT20X20 = 2,

        [EnumSerializationName("FNT30x30")]
        FNT30X30 = 3,

        [EnumSerializationName("FNT50x50")]
        FNT50X50 = 4,
    }
}
