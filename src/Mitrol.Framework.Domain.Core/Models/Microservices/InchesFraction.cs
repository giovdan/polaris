namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Class for inches fractional represention
    /// </summary>
    public class InchesFraction
    {
        private static int CONV_FEET_INCHES = 12;
        private static string REGEX_FRACTION = "^\\s*(?<neg>-)?\\s*(?:(?<ft>\\d*(?!\"))(?=')'-?)?\\s* (?:(?<in>[0-9]|1[0-1])\"?)?\\s*(?:(?<n>\\d+)/(?<d>\\d+))?$|^\\s*(?<neg>-)?\\s*(?:(?<in>\\d*)(?=\")\")?\\s*(?:(?<n>\\d+)/(?<d>\\d+))?$";

        /// <summary>
        /// Inches value
        /// </summary>
        public decimal Value { get; private set; }
        /// <summary>
        /// Inches part (Feet and Inches)
        /// </summary>
        public decimal Inches { get; private set; }
        /// <summary>
        /// Fractional Part
        /// </summary>
        public int Numerator { get; private set; }
        public int Denominator { get; private set; }
        public InchesFraction(int feet, int inches, int numerator, int denominator) : this(inches + (feet * CONV_FEET_INCHES), numerator, denominator)
        {
        }

        public InchesFraction(int inches, int numerator, int denominator)
        {
            Inches = inches;
            Numerator = numerator;
            Denominator = denominator;
            Value = inches + ((decimal)Numerator) / Denominator;
        }


        public InchesFraction(string value)
        {
            Match matches = Regex.Match(value, REGEX_FRACTION);
            if (matches.Success)
            {
                Inches = int.Parse(matches.Groups["ft"].Value) * 12 + int.Parse(matches.Groups["in"].Value);
                if (int.TryParse(matches.Groups["n"].Value, out int numerator))
                {
                    Numerator = numerator;
                }

                if (int.TryParse(matches.Groups["d"].Value, out int denominator))
                {
                    Denominator = denominator;
                }


                Value = Inches;

                if (Numerator > 0 && Denominator > 0)
                    Value += ((decimal)Numerator) / Denominator;
            }
        }

        public InchesFraction(decimal value)
        {
            Value = value;
            Convert();
        }


        public override string ToString()
        {
            decimal feet = Math.Floor(Inches / 12);
            decimal inches = Numerator > 0 && Denominator > 0 ? Math.Floor(Inches - (feet * 12)) : Inches - (feet * 12);

            StringBuilder value = new StringBuilder();
            if (feet != 0)
                value.Append($"{feet}' ");

            if (inches != 0)
                value.Append($"{inches}\"");

            if (Numerator > 0 && Denominator > 0)
                value.Append($"{Numerator}/{Denominator}");

            return value.ToString();

        }


        private void Convert()
        {
            Inches = Math.Floor(Value);
            decimal fract = Value % 1;
            Numerator = 0;
            Denominator = 0;

            if (fract > 0)
            {
                bool founded = false;
                //Check if is a regular fraction
                for (int power = 2; power <= 64; power = power * 2)
                {
                    decimal exactFraction = (decimal)1 / power;
                    if (fract % exactFraction == 0)
                    {
                        Numerator = (int)(fract / exactFraction);
                        Denominator = power;
                        founded = true;
                        break;
                    }
                }

                if (!founded)
                    Inches += fract;
            }
        }
    }
}
