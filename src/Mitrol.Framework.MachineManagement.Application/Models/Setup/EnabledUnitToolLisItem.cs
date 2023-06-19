namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Newtonsoft.Json;

    public class EnabledUnitToolLisItem
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }
        [JsonProperty("Percentage")]
        public int Percentage { get; set; }
        [JsonIgnore()]
        public long InnerId { get; internal set; }
    }

    
}
