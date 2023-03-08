namespace Mitrol.Framework.Domain.Core.Models.Microservices
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class BaseNotificationListItem
    {
        [JsonProperty("Type")]
        public NotificationTypeEnum Type { get; set; }

        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }

        [JsonProperty("DescriptionLocalizationKey")]
        public string DescriptionLocalizationKey { get; set; }

        [JsonProperty("Number")]
        public int Number { get; set; }

        [JsonProperty("Parameter")]
        public int Parameter { get; set; }

        [JsonProperty("AdditionalInfo")]
        public Dictionary<string, object> AdditionalInfos { get; set; }

        [JsonProperty("Arguments")]
        public Dictionary<string, string> Arguments { get; set; }

        [JsonProperty("CausesAndSolutions")]
        public CauseSolutionPair[] CausesAndSolutions { get; set; }
    }
}
