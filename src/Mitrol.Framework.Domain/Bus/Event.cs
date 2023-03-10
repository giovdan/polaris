namespace Mitrol.Framework.Domain.Bus
{
    using Newtonsoft.Json;
    using System;

    public abstract class Event
    {
        [JsonProperty("EventDate")]
        public DateTime EventDate { get; set; }
        [JsonProperty("TimeZoneId")]
        public string TimeZoneId { get; set; }
        
        protected Event()
        {
            EventDate = DateTime.UtcNow;
            TimeZoneId = TimeZoneInfo.Local.Id;
        }
    }
}