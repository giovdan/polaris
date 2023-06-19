namespace Mitrol.Framework.Domain.Production.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Class for Lite Piece Operation used in Separation Cuts calculation
    /// </summary>
    public class LitePieceOperationItem: IEntityWithAttributeValues
    {
        [JsonProperty("LineNumber")]
        public int LineNumber { get; set; }
        [JsonProperty("OperationType")]
        public OperationTypeEnum OperationType { get; set; }
        [JsonProperty("Attributes")]
        public Dictionary<DatabaseDisplayNameEnum, object> Attributes { get; set; }
    }

    /// <summary>
    /// class for manage Piece Operation
    /// (Path Operation include Children (list of vertex))
    /// </summary>
    public class PieceOperationItem : LitePieceOperationItem
    {
        [JsonIgnore()]
        public long Id { get; set; }
        [JsonProperty("ToBeSkipped")]
        public bool ToBeSkipped { get; set; }
        [JsonProperty("Level")]
        public int Level { get; set; }
        [JsonProperty("ParentId")]
        public long? ParentId { get; set; }
        [JsonProperty("Children")]
        public List<PieceOperationAdditionalItem> AdditionalItems { get; set; }
        [JsonProperty("ParentLineNumber")]
        public int? ParentLineNumber { get; set; }
        [JsonIgnore()]
        public OperationRowStatusEnum Status { get; set; }
        public PieceOperationItem()
        {
            Attributes = new Dictionary<DatabaseDisplayNameEnum, object>();
            AdditionalItems = new List<PieceOperationAdditionalItem>();
        }

    }


}
