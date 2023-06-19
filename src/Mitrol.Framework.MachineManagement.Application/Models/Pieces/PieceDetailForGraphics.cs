namespace Mitrol.Framework.MachineManagement.Application.Models.Production
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class PieceDetailForGraphics
    {
        [JsonProperty("Attributes")]
        public Dictionary<DatabaseDisplayNameEnum, object> Attributes { get; set; }
        [JsonProperty("Operations")]
        public IEnumerable<PieceOperationDetailForGraphics> Operations { get; set; }

        public PieceDetailForGraphics()
        {
            Attributes = new Dictionary<DatabaseDisplayNameEnum, object>();;
        }
    }

    public class PieceOperationDetailForGraphics
    {
        [JsonProperty("OperationType")]
        public string OperationType { get; set; }
        [JsonProperty("LineNumber")]
        public int LineNumber { get; set; }
        [JsonProperty("ParentLineNumber")]
        public int ParentLineNumber { get; set; }
        [JsonProperty("ToBeSkipped")]
        public bool ToBeSkipped { get; set; }
        [JsonProperty("Attributes")]
        public Dictionary<DatabaseDisplayNameEnum, object> Attributes { get; set; }
        [JsonProperty("AdditionalItems")]
        public IEnumerable<AdditionalItemForGraphics> AdditionalItems { get; set; }

        public PieceOperationDetailForGraphics()
        {
            Attributes = new Dictionary<DatabaseDisplayNameEnum, object>();
            AdditionalItems = Array.Empty<AdditionalItemForGraphics>();
        }
    }

    public class AdditionalItemForGraphics
    {
        [JsonProperty("Index")]
        public int Index { get; set; }
        [JsonProperty("OperationType")]
        public OperationTypeEnum OperationType { get; set; }
        [JsonProperty("Attributes")]
        public Dictionary<DatabaseDisplayNameEnum, object> Attributes { get; set; }

        public AdditionalItemForGraphics()
        {
            Attributes = new Dictionary<DatabaseDisplayNameEnum, object>();
        }
    }
}
