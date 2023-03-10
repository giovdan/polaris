namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;
    using System;
    using System.ComponentModel;

    [Flags]
    public enum AttributeTypeEnum : int
    {
        //[DatabaseDisplayName("Geometric")]
        [Description("Attributo geometrico")]
        Geometric = 1,
        //[DatabaseDisplayName("Process")]
        [Description("Attributo di processo")]
        Process = 2,
        //[DatabaseDisplayName("Identifier")]
        [Description("Attributo Identificativo")]
        Identifier = 4,
        //[DatabaseDisplayName("Generic")]
        [Description("Attributo generico")]
        Generic = 8,
        //[DatabaseDisplayName("All")]
        [Description("Tutti gli attributi")]
        All = Geometric | Process | Identifier | Generic
    }


}