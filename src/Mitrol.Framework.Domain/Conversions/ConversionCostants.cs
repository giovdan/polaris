namespace Mitrol.Framework.Domain.Conversions
{
    public static class ConversionCostants
    {
        #region < Conversion Constants >
        // Fattore di conversione millimetri/inch
        public const decimal CONV_MM_INCH = 25.4M;
        // Fattore di conversione bar/psi
        public const decimal CONV_BAR_PSI = 14.5037738M;
        // Fattore di conversione peso specifico da Kg/dm*3 a Pound/foot*3
        public const decimal CONV_KGDM3_POUNDFT3 = 62.427971M;
        // Fattore di conversione sezione da cm*2 a inch*2
        public const decimal CONV_MM2_INCH2 = 645.16M;
        // Fattore di conversione sezione da cm*2 a inch*2
        public const decimal CONV_CM2_INCH2 = 6.4516M;
        // Fattore di conversione peso lineare da kg/mt a Pound/foot
        public const decimal CONV_KGMT_POUNDFT = 1.488164M;
        // Fattore di conversione peso da kg a Pound
        public const decimal CONV_KG_POUND = 2.204622M;
        // Fattore di conversione supeficie da m**2 a foot**2
        public const decimal CONV_M2_FOOT2 = 0.092903M;
        // Fattore di conversione da metri a inches
        public const decimal CONV_MT_INCH = 39.37M;
        // Fattore di conversione da mt/min a feet/min
        public const decimal CONV_MTMIN_FEETMIN = (1000M / CONV_MM_INCH) * 12M;
        public const decimal CONV_MT_FEET = 3.281M;
        public const decimal CONV_MT2_FEET2 = 10.7639M;
        #endregion < Converion Constants >
    }
}
