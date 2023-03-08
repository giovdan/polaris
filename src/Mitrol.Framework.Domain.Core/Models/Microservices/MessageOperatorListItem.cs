namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Newtonsoft.Json;
    using System;

    public class MessageOperatorListItem: BaseNotificationListItem
    {
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("NotifiedOn")]
        public DateTime NotifiedOn { get; set; }

        [JsonIgnore()]
        public string TimeZoneId { get; set; }
    }
}
