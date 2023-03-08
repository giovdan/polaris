namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Interfaces;
    using Newtonsoft.Json;
    using System;

    /// <summary>
    /// Classe che rappresenta un elemnto della Lista degli allarmi risolti
    /// </summary>
    public class NotificationListItem: BaseNotificationListItem, IEntityWithImage
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("ElapsedTime")]
        public long ElapsedTime { get; set; }
        [JsonProperty("ImageCode")]
        public string ImageCode { get; set; }
        [JsonProperty("NotifiedOn")]
        public DateTime NotifiedOn { get; set; }
        [JsonProperty("ResolvedOn")]
        public DateTime? ResolvedOn { get; set; }
        [JsonIgnore()]
        public string TimeZoneId { get; set; }
    }

    public static class NotificationListItemCustomMappings
    {
        /// <summary>
        /// Apply Custom Mapping on Notification Item
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public static NotificationListItem ApplyCustomMapping(this NotificationListItem notification
                                , bool isElapsedTime)
        {
            //Se bisogna calcolare l'elapsed time (notification resolved)
            if (isElapsedTime)
            {
                //ElpasedTime contiene il life time della notifica
                var elapsedTime = notification.ResolvedOn
                    .GetValueOrDefault().Subtract(notification.NotifiedOn);

                notification.ElapsedTime = Convert.ToInt64(elapsedTime.TotalSeconds);
            }
            else
            {
                //ElpasedTime contiene l'ora della notifica
                notification.ElapsedTime = Convert.ToInt64(notification.NotifiedOn
                            .Subtract(DateTime.UtcNow).TotalSeconds);
            }

            return notification;
        }
    }
}
