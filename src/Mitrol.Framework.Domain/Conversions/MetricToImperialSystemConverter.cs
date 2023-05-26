namespace Mitrol.Framework.Domain.Conversions
{
    /// <summary>
    /// Class that convert from Imperial System to Metric System
    /// </summary>
    public class MetricToImperialSystemConverter: Converter
    {
        /// <summary>
        /// Converts millimeters to inches (Length conversion)
        /// </summary>
        /// <param name="millimeters"></param>
        /// <returns></returns>
        public override decimal ConvertLength(decimal millimeters)
        {
            return millimeters / ConversionCostants.CONV_MM_INCH;
        }


        /// <summary>
        /// Converts Bar to PSI (Pressure conversion)
        /// </summary>
        /// <param name="bar"></param>
        /// <returns></returns>
        public override decimal ConvertPressure(decimal bar)
        {
            return bar * ConversionCostants.CONV_BAR_PSI;
        }

        /// <summary>
        /// Converts Kg/dm^3 to Pound/Feet^3 (Specific Weight conversion)
        /// </summary>
        /// <param name="kgdm3"></param>
        /// <returns></returns>
        public override decimal ConvertSpecificWeight(decimal kgdm3)
        {
            return kgdm3 * ConversionCostants.CONV_KGDM3_POUNDFT3;
        }

        /// <summary>
        /// Converts Kg/mt to Pound/foot (Linear weight conversion)
        /// </summary>
        /// <param name="kgmt"></param>
        /// <returns></returns>
        public override decimal ConvertLinearWeight(decimal kgmt)
        {
            return kgmt * ConversionCostants.CONV_KGMT_POUNDFT;
        }

        /// <summary>
        /// Converts kg to pounds (Weight conversion)
        /// </summary>
        /// <param name="kg"></param>
        /// <returns></returns>
        public override decimal ConvertWeight(decimal kg)
        {
            return kg * ConversionCostants.CONV_KG_POUND;
        }

        /// <summary>
        /// Converts mm^2 to inches^2 (Section conversion)
        /// </summary>
        /// <param name="cm2"></param>
        /// <returns></returns>
        public override decimal ConvertArea(decimal mm2)
        {
            return mm2 / ConversionCostants.CONV_MM2_INCH2;
        }

        /// <summary>
        /// Converts cm^2 to inches^2 (Section conversion)
        /// </summary>
        /// <param name="cm2"></param>
        /// <returns></returns>
        public override decimal ConvertSection(decimal cm2)
        {
            return cm2 / ConversionCostants.CONV_CM2_INCH2;
        }

        /// <summary>
        /// Converts m^2 to foot^2 (Surface convertion)
        /// </summary>
        /// <param name="m2"></param>
        /// <returns></returns>
        public override decimal ConvertSurface(decimal m2)
        {
            return m2 / ConversionCostants.CONV_M2_FOOT2;
        }

        /// <summary>
        /// Converts mm/min to inch/min
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override decimal ConvertSpeed(decimal value)
        {
            return value / ConversionCostants.CONV_MM_INCH;
        }

        public override decimal ConvertPeripheralSpeed(decimal value)
        {
            return value * ConversionCostants.CONV_MTMIN_FEETMIN;
        }

        /// <summary>
        /// Converts mt to inches
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override decimal ConvertDistance(decimal value)
        {
            return value * ConversionCostants.CONV_MT_INCH;
        }

        /// <summary>
        /// Converts m²/m to feet²/feet
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override decimal ConvertLinearSurface(decimal value)
        {
            return value * ConversionCostants.CONV_MT_FEET;
        }
    }
}
