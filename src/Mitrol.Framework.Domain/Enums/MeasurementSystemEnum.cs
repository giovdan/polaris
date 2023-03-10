namespace Mitrol.Framework.Domain.Enums
{
    public enum MeasurementSystemEnum
    {
        MetricSystem = 1,
        ImperialSystem = 2,
        FractionalImperialSystem = 3
    }

    public static class MeasurementSystemEnumExtensions
    {
        public static MeasurementSystemEnum ToRealConversionSystem(this MeasurementSystemEnum conversionSystem)
        {
            if (conversionSystem == MeasurementSystemEnum.FractionalImperialSystem)
                return MeasurementSystemEnum.ImperialSystem;

            return conversionSystem;
        }
    }
}
