namespace Mitrol.Framework.Domain.Models
{
    using Newtonsoft.Json;

    public class CultureItem
    {
        [JsonProperty(PropertyName = "id")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Description { get; set; }
    }
}