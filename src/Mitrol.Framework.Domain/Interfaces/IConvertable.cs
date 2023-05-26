namespace Mitrol.Framework.Domain.Interfaces
{
    using Mitrol.Framework.Domain.Enums;

    public interface IConvertable
    {
        AttributeDataFormatEnum ItemDataFormat { get; set; }
        string UMLocalizationKey { get; set; }
        int DecimalPrecision { get; set; }
    }
}
