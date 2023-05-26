namespace Mitrol.Framework.Domain.Conversions
{
    /// <summary>
    /// Class that convert from Imperial System to Metric System
    /// </summary>
    public class ImperialToMetricSystemConverter: Converter
    {
        /// <summary>
        /// Converts inches to millimeters (Length conversion)
        /// </summary>
        /// <param name="inches"></param>
        /// <returns></returns>
        public override decimal ConvertLength(decimal inches)
        {
            return inches * ConversionCostants.CONV_MM_INCH;
        }


        /// <summary>
        /// Converts PSI to BAR (Pressure conversion)
        /// </summary>
        /// <param name="psi"></param>
        /// <returns></returns>
        public override decimal ConvertPressure(decimal psi)
        {
            return psi / ConversionCostants.CONV_BAR_PSI;
        }

        /// <summary>
        /// Converts Pound/Feet^3 to Kg/dm^3 (Specific Weight conversion)
        /// </summary>
        /// <param name="poundfeet3"></param>
        /// <returns></returns>
        public override decimal ConvertSpecificWeight(decimal poundfeet3)
        {
            return poundfeet3 / ConversionCostants.CONV_KGDM3_POUNDFT3;
        }

        /// <summary>
        /// Converts Pound/foot to Kg/mt 
        /// </summary>
        /// <param name="poundfoot"></param>
        /// <returns></returns>
        public override decimal ConvertLinearWeight(decimal poundfoot)
        {
            return poundfoot * ConversionCostants.CONV_KGMT_POUNDFT;
        }

        /// <summary>
        /// Converts pounds to kg 
        /// </summary>
        /// <param name="pounds"></param>
        /// <returns></returns>
        public override decimal ConvertWeight(decimal pounds)
        {
            return pounds / ConversionCostants.CONV_KG_POUND;
        }

        /// <summary>
        /// Converts inches^2 to mm^2 (Section conversion)
        /// </summary>
        /// <param name="inches2"></param>
        /// <returns></returns>
        public override decimal ConvertArea(decimal inches2)
        {
            return inches2 * ConversionCostants.CONV_MM2_INCH2;
        }

        /// <summary>
        /// Converts inches^2 to cm^2 (Section conversion)
        /// </summary>
        /// <param name="inches2"></param>
        /// <returns></returns>
        public override decimal ConvertSection(decimal inches2)
        {
            return inches2 * ConversionCostants.CONV_CM2_INCH2;
        }

        /// <summary>
        /// Converts foot^2 to mt^2
        /// </summary>
        /// <param name="foot2"></param>
        /// <returns></returns>
        public override decimal ConvertSurface(decimal foot2)
        {
            return foot2 * ConversionCostants.CONV_M2_FOOT2;
        }

        /// <summary>
        /// Converts inc/min to mm/min 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override decimal ConvertSpeed(decimal value)
        {
            return value * ConversionCostants.CONV_MM_INCH;
        }

        /// <summary>
        /// Converts feet/min to mt/min
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override decimal ConvertPeripheralSpeed(decimal value)
        {
            return value / ConversionCostants.CONV_MTMIN_FEETMIN;
        }

        /// <summary>
        /// Converts inch to meters
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override decimal ConvertDistance(decimal value)
        {
            return value / ConversionCostants.CONV_MT_INCH;
        }

        /// <summary>
        /// converts feet2/feet to mt2/mt
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override decimal ConvertLinearSurface(decimal value)
        {
            return value / ConversionCostants.CONV_MT_FEET;
        }
    }
}
