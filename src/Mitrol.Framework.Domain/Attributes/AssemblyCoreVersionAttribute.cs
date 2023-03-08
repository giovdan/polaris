namespace Mitrol.Framework.Domain.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class AssemblyCoreVersionAttribute : Attribute
    {
        public string Version { get; }

        public AssemblyCoreVersionAttribute(string version)
        {
            Version = version;
        }
    }
}
