namespace Mitrol.Framework.Domain.Attributes
{
    using System;

    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class AssemblyDatabaseVersionAttribute : Attribute
    {
        public int DatabaseVersion { get; }

        public AssemblyDatabaseVersionAttribute(int dbVersionNumber)
        {
            DatabaseVersion = dbVersionNumber;
        }
    }
}
