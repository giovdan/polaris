namespace Mitrol.Framework.MachineManagement.Domain.Attributes
{
    using Mitrol.Framework.Domain.Core.Enums;
    using System;

    [AttributeUsage(AttributeTargets.All)]
    public class StatusColorAttribute : Attribute
    {
        public StatusColorEnum StatusColor { get; internal set; }

        public StatusColorAttribute(StatusColorEnum statusColor)
        {
            StatusColor = statusColor;
        }
    }

}