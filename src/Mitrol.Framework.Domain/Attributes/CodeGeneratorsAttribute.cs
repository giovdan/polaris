namespace Mitrol.Framework.Domain.Attributes
{
    using Mitrol.Framework.Domain.Enums;
    using System;

    [AttributeUsage(AttributeTargets.All)]
    public class CodeGeneratorsAttribute: Attribute
    {
        public DatabaseDisplayNameEnum[] CodeGenerators { get; set; }
        public CodeGeneratorsAttribute(params DatabaseDisplayNameEnum[] codeGenerators)
        {
            CodeGenerators = codeGenerators;
        }

    }
}
