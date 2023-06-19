namespace Mitrol.Framework.MachineManagement.Application.Models.Setup
{
    using Mitrol.Framework.Domain.Interfaces;
    using Newtonsoft.Json;

    public class PlasmaToolComponentWizardStep: IEntityWithImage
    {
        [JsonProperty("ImageCode")]
        public string ImageCode { get; set; }
        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }
    }
}
