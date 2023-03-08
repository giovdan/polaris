using System;

namespace Mitrol.Framework.Domain.Core.Attributes
{
    /// <summary>
    /// Attribute used for disable auditing on a class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DisableAuditing : Attribute
    {
    }
}