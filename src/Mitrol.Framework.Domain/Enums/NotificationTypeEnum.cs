namespace Mitrol.Framework.Domain.Enums
{
    using Mitrol.Framework.Domain.Attributes;

    public enum NotificationTypeEnum
    {
        //[DatabaseDisplayName("ALL")]
        Alarm = 1,
        //[DatabaseDisplayName("MSG")]
        Message = 2,
        //[DatabaseDisplayName("OPERMSG")]
        Info = 4,
        All = Alarm | Message,
    }
}
