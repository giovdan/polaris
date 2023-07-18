
namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class ProfileTypeFilters
    {
        [JsonProperty("ProfileType")]
        public ProfileTypeEnum ProfileType { get; set; }
        [JsonIgnore()]
        public MeasurementSystemEnum ConversionSystem { get; set; }
    }

}
