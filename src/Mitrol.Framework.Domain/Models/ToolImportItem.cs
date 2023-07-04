namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Class for import tools
    /// </summary>
    /// <typeparam name="TAttribute"></typeparam>
    public class ToolImportItem<TAttribute>
    {
        [JsonProperty("Code")]
        public string Code { get; set; }
        [JsonProperty("Type")]
        public string Type { get; set; }
        [JsonProperty("ToolUnitMask")]
        public ToolUnitMaskEnum ToolUnitMask { get; set; }
        [JsonProperty("IsEnabled")]
        public bool IsEnabled { get; set; }
        [JsonProperty("IsManual")]
        public bool IsManual { get; set; }
        [JsonProperty("ToolManagementId")]
        public int ToolManagementId { get; set; }
        [JsonProperty("Attributes")]
        public Dictionary<string, TAttribute> Attributes { get; set; }
        [JsonProperty("Identifiers")]
        public Dictionary<string, string> Identifiers { get; set; }
        [JsonIgnore()]
        public long WarehouseId { get; set; }

        public ToolImportItem()
        {
            Attributes = new Dictionary<string, TAttribute>();
            Identifiers = new Dictionary<string, string>();
            ToolUnitMask = ToolUnitMaskEnum.None;
            IsEnabled = true;
            IsManual = false;
        }
    }
}
