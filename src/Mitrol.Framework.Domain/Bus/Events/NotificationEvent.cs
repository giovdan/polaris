namespace Mitrol.Framework.Domain.Bus.Events
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class NotificationEvent : Event
    {
        [JsonProperty("LoggedUser")]
        public string LoggedUser { get; set; }

        [JsonProperty("MachineName")]
        public string MachineName { get; set; }

        [JsonProperty("Type")]
        public NotificationTypeEnum Type { get; set; }

        [JsonProperty("Number")]
        public int Number { get; set; }

        [JsonProperty("Parameter")]
        public int Parameter { get; set; }

        [JsonProperty("ResolvedOn")]
        public DateTime? ResolvedOn { get; set; }

        [JsonProperty("AdditionalInfos")]
        public Dictionary<string, object> AdditionalInfos { get; set; }
        
        [JsonProperty("Arguments")]
        public Dictionary<string, string> Arguments { get; set; }

        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }

        [JsonProperty("DescriptionLocalizationKey")]
        public string DescriptionLocalizationKey { get; set; }

        //public CauseSolutionPair[] CausesAndSolutions { get; set; }
    }
}