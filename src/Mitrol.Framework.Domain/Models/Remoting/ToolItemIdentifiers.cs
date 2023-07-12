namespace Mitrol.Framework.Domain.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Production.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ToolItemIdentifiers
    {
        /// <summary>
        /// Tool Master Id
        /// </summary>
        [JsonProperty("HashCode")]
        public string HashCode { get; set; }

        /// <summary>
        /// Tool Management Id
        /// </summary>
        [JsonProperty("ToolManagementId")]
        public int ToolManagementId { get; set; }

        /// <summary>
        /// Tipologia del tool
        /// </summary>
        [JsonProperty("ToolType")]
        public ToolTypeEnum ToolType { get; set; }

        /// <summary>
        /// Abilitazione del tool
        /// </summary>
        [JsonProperty("Status")]
        public EntityStatusEnum Status { get; set; }

        /// <summary>
        /// Unità per il quale il tool è abilitato
        /// </summary>
        [JsonProperty("ToolUnitMask")]
        public ToolUnitMaskEnum UnitEnablingMask { get; set; }

        /// <summary>
        /// Lista di attributi identificatori
        /// </summary>
        [JsonProperty("Identifiers")]
        public Dictionary<DatabaseDisplayNameEnum, object> Identifiers { get; set; }

    }
}
