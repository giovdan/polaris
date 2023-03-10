namespace Mitrol.Framework.Domain.Bus.Events
{
    using Mitrol.Framework.Domain.Bus.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public abstract class SubscribleEvent<TData> : Event, ISubscribleEvent
    {
        public SubscribleEvent(SubscribableEventEnum eventId)
        {
            EventCode = eventId;
            ConversionSystem = MeasurementSystemEnum.MetricSystem;
        }

        [JsonProperty(PropertyName = "EventCode")]
        public SubscribableEventEnum EventCode { get; set; }

        [JsonProperty("DataRefreshed")]
        public TData Data { get; set; }

        [JsonProperty("ConversionSystem")]
        public MeasurementSystemEnum ConversionSystem { get; set; }
    }
}
