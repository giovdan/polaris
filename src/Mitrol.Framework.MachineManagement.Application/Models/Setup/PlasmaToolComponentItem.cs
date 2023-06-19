namespace Mitrol.Framework.MachineManagement.Application.Models.Setup
{
    using Mitrol.Framework.Domain.Interfaces;
    using Newtonsoft.Json;

    public class PlasmaToolComponentItem: IEntityWithImage
    {
        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get;  set; }
        [JsonProperty("Value")]
        public string Value { get; set; }
        [JsonProperty("ToBeReplaced")]
        public bool ToBeReplaced { get; set; }
        [JsonProperty("ImageCode")]
        public string ImageCode { get; set; }

    }
}
