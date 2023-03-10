namespace Mitrol.Framework.Domain.Bus.Events
{
   using Mitrol.Framework.Domain.Bus.Enums;

    public interface ISubscribleEvent
    {
        SubscribableEventEnum EventCode { get; set; }
    }
}
