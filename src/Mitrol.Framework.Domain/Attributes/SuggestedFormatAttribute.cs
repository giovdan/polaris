namespace Mitrol.Framework.Domain.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class SuggestedFormatAttribute: Attribute
    {
        public int Level { get; set; }
        public string SuggestedFormat { get; set; }
        public SuggestedFormatAttribute(string suggestedFormat, int level = 1)
        {
            Level = level;
            SuggestedFormat = suggestedFormat;
        }
    }
}
