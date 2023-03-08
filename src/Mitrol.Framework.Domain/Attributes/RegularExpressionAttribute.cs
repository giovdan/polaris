namespace Mitrol.Framework.Domain.Attributes
{
    using System;

    public sealed class RegularExpressionAttribute : Attribute
    {
        public string RegularExpression { get; }

        public RegularExpressionAttribute(string regexPattern)
        {
            RegularExpression = regexPattern;
        }
    }
}