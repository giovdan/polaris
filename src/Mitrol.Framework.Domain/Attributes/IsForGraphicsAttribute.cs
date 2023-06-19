namespace Mitrol.Framework.Domain.Attributes
{
    using System;
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class IsForGraphicsAttribute:Attribute
    {
        public bool IsForGraphics { get; set; }

        public IsForGraphicsAttribute(bool isForGraphics = false)
        {
            IsForGraphics = isForGraphics;
        }
    }
}
