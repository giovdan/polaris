namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class AttributeStatus
    {
        [JsonProperty("ErrorLocalizationKey")]
        public string ErrorLocalizationKey { get; set; }

        [JsonProperty("Status")]
        public EntityStatusEnum Status { get; set; }

        public AttributeStatus()
        {
            Status = EntityStatusEnum.Available;
            ErrorLocalizationKey = string.Empty;
        }
    }
}
