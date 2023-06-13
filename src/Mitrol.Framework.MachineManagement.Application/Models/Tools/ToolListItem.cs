namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Core.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class ToolBaseItem
    {
        protected static TypeConverter s_toolTypeEnumTypeConverter = TypeDescriptor.GetConverter(typeof(ToolTypeEnum));

        public ToolBaseItem()
        {
            CodeGenerators = Array.Empty<CodeGeneratorItem>();
        }
        /// <summary>
        /// Identificativo del tool
        /// </summary>
        [JsonIgnore()]
        public long InnerId { get; set; }

        /// <summary>
        /// Tool Management Id
        /// </summary>
        [JsonProperty("Id")]
        public int Id { get; set; }
        /// <summary>
        /// Tipologia del tool
        /// </summary>
        [JsonProperty("ToolType")]
        public ToolTypeEnum ToolType { get; set; }

        [JsonProperty("Status")]
        public EntityStatusEnum Status { get; set; }

        [JsonProperty("Percentage")]
        public int? Percentage { get; set; } //TODO: Calcolo percentage

        /// <summary>
        /// Codice identificativo del tool
        /// </summary>
        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("StatusLocalizationKey")]
        public string StatusLocalizationKey { get; set; }


        [JsonProperty("UsedForExecution")]
        public bool UsedForExecution { get; set; }


        /// <summary>
        /// Lista di attributi per la generazione del codice del tool
        /// </summary>
        [JsonIgnore()]
        public IEnumerable<CodeGeneratorItem> CodeGenerators { get; set; }

        [JsonProperty("HashCode")]
        public string HashCode { get; set; }

        [JsonIgnore()]
        public virtual PlantUnitEnum PlantUnit { get; set; }

        public ToolListItem Clone() => this.MemberwiseClone() as ToolListItem;

    }

    /// <summary>
    /// Classe che rappresenta un elemento della lista dei tools
    /// </summary>
    public class ToolListItem: ToolBaseItem
    {
        public ToolListItem():base()
        {
            Identifiers = new List<IdentifierDetailItem>();
        }

        [JsonIgnore()]
        public IEnumerable<IdentifierDetailItem> Identifiers { get; set; }
    }

    public class LastUpdatedToolListItem: ToolListItem
    {
        [JsonProperty("UpdatedOn")]
        public DateTime UpdatedOn { get; set; }

        [JsonProperty("ImageCode")]
        public string ImageCode { get; set; }

        public new LastUpdatedToolListItem Clone() => this.MemberwiseClone() as LastUpdatedToolListItem;
    }
}
