namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class DecimalPrecisionAttribute : Attribute
    {
        public MeasurementSystemEnum SystemOfMeasure { get; internal set; }
        public short NumberOfDigits { get; internal set; }
        public short NumberOfDigitsForLabel { get; internal set; }

        public DecimalPrecisionAttribute(MeasurementSystemEnum systemOfMeasure, short numberOfDigits, short numberOfDigitsForLabel = 0)
        {
            NumberOfDigits = numberOfDigits;
            SystemOfMeasure = systemOfMeasure;
            if (numberOfDigitsForLabel == 0)
                NumberOfDigitsForLabel = numberOfDigits;
            else
                NumberOfDigitsForLabel = numberOfDigitsForLabel;
        }
    }
}
