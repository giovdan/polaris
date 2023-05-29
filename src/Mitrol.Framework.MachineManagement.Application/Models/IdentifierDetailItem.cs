namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;

    public class IdentifierDetailItem: DetailItem<string>
    {
        [JsonIgnore()]
        public AttributeDataFormatEnum ItemDataFormat { get; set; }

        [JsonIgnore()]
        public long MasterId { get; set; }

        [JsonProperty("IsMainFilter")]
        public bool IsMainFilter { get; set; }

        public IdentifierDetailItem Clone() => this.MemberwiseClone() as IdentifierDetailItem;
    }
    
}