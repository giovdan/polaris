namespace Mitrol.Framework.Domain.Production.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Interfaces;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    /// <summary>
    /// Class for manage Piece Operation additional items like Vertex for PATH Operation
    /// </summary>
    public class PieceOperationAdditionalItem : IEntityWithImage, IEntityWithAttributeValues
    {
        [JsonIgnore()]
        public long Id { get; set; }

        [JsonProperty("Index")]
        public int Index { get; set; }

        [JsonProperty("ImageCode")]
        public string ImageCode { get; set; }

        [JsonProperty("OperationType")]
        public OperationTypeEnum OperationType { get; set; }

        [JsonProperty("Attributes")]
        public Dictionary<DatabaseDisplayNameEnum, object> Attributes { get; set; }

        [JsonIgnore()]
        public OperationRowStatusEnum Status { get; set; }

        public PieceOperationAdditionalItem()
        {
            Status = OperationRowStatusEnum.UnChanged;
        }
    }
}