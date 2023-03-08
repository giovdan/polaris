namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class LastUpdatedBaseItem
    {
        [JsonProperty("Id")]
        public long Id { get; set; }
        [JsonProperty("Code")]
        public string Code { get; set; }
        [JsonProperty("Attributes")]
        public IEnumerable<LocalizedInfoItem<string>> Attributes { get; set; }
        [JsonProperty("UpdatedOn")]
        public DateTime UpdatedOn { get; set; }

        public LastUpdatedBaseItem()
        {
            Attributes = Array.Empty<LocalizedInfoItem<string>>();
        }

    }
}
