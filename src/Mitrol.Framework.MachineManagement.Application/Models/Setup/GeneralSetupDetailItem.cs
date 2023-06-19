
namespace Mitrol.Framework.MachineManagement.Application.Models.Setup
{
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System;


    public class GeneralSetupBaseGroupItem
    {
        public GeneralSetupBaseGroupItem()
        {
            Details = Array.Empty<AttributeDetailItem>();
        }

        [JsonProperty("Priority")]
        public int Priority { get; set; }

        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }

        [JsonProperty("Details")]
        public AttributeDetailItem[] Details { get; set; }

        [JsonProperty("Hidden")]
        public bool Hidden { get; set; }
    }

    public class GeneralSetupDetailItem
    {
        [JsonProperty("Attributes")]
        public AttributeDetailItem[] Attributes { get; set; }

        [JsonProperty("ReadOnlyAttributesGroups")]
        public GeneralSetupBaseGroupItem[] ReadOnlyAttributesGroups { get; set; }
        
        [JsonProperty("ImageCode")]
        public string ImageCode { get; set; }

        [JsonProperty("UpdatedData")]
        public bool UpdatedData { get; set; }

        public GeneralSetupDetailItem()
        {
            ReadOnlyAttributesGroups = Array.Empty<GeneralSetupBaseGroupItem>();
            Attributes = Array.Empty<AttributeDetailItem>();
            UpdatedData = true;
        }
    }
}
