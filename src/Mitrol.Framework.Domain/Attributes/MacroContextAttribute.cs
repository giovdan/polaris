

namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Macro;
    using System;

    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    public class MacroContextAttribute : Attribute
    {
        public DatabaseDisplayNameEnum DatabaseDisplayName { get; set; }
        public ExternalInterfaceNameEnum MacroContextDataEnum { get; set; }

        public MacroTypeEnum MacroTypeEnum { get; set; }

        public MacroContextAttribute(MacroTypeEnum macroTypeEnum,
                                              ExternalInterfaceNameEnum macroContextDataEnum,
                                              DatabaseDisplayNameEnum databaseDisplayName=0)
        {
            MacroContextDataEnum = macroContextDataEnum;
            MacroTypeEnum = macroTypeEnum;
            DatabaseDisplayName = databaseDisplayName;
        }
    }
}
