
namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using Mitrol.Framework.Domain.Core;
    using System.ComponentModel;

    [TypeConverter(typeof(EnumCustomNameTypeConverter))]
    public enum AttributeDefinitionEnum
    {
    }
}
