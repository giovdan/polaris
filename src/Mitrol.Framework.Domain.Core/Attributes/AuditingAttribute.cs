using System;

namespace Mitrol.Framework.Domain.Core.Attributes
{
    /// <summary>
    /// Attribute used for enable auditing on a class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class AuditingAttribute : Attribute
    {
        public bool Enabled { get; set; }
        public AuditingAttribute(bool enabled = false)
        {
            Enabled = enabled;
        }
    }
}