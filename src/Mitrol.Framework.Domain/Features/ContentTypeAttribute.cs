namespace Mitrol.Framework.Domain.Features
{
    using System;

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class ContentTypeAttribute : Attribute
    {
        public Type ContentType { get; private set; }
        public object DefaultValue { get; private set; }

        public ContentTypeAttribute(Type contentType, object defaultValue)
        {
            ContentType = contentType;
            DefaultValue = defaultValue;
        }
    }


}
