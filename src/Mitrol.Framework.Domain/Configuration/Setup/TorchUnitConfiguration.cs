namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class TorchUnitConfiguration : UnitConfiguration, IUnitConfiguration
    {
        internal const string s_typeJsonName = "Type";
        internal const string s_nodeJsonName = "Node";
        internal const string s_pathJsonName = "Path";
        internal const string s_slaveTorchCountJsonName = "SlaveTorchCount";
        
        public TorchUnitConfiguration() { }

        [JsonConstructor]
        public TorchUnitConfiguration([JsonProperty(s_unitJsonName)] UnitEnum? unit,
                                      [JsonProperty(s_gridPositionJsonName)] GridPosition gridPosition,
                                      [JsonProperty(s_typeJsonName)] TorchTypeEnum? type,
                                      [JsonProperty(s_nodeJsonName)] NodeReference node,
                                      [JsonProperty(s_pathJsonName)] short? path,
                                      [JsonProperty(s_slaveTorchCountJsonName)] byte? slaveTorchCount)
            : base(unit, gridPosition)
        {
            Type = type;
            Node = node;
            Path = path;
            SlaveTorchCount = slaveTorchCount;
        }

        [JsonProperty(s_typeJsonName)]
        public TorchTypeEnum? Type { get; protected set; }

        [JsonProperty(s_nodeJsonName)]
        public NodeReference Node { get; protected set; }

        [JsonProperty(s_pathJsonName)]
        public short? Path { get; protected set; }

        [JsonProperty(s_slaveTorchCountJsonName)]
        public byte? SlaveTorchCount { get; protected set; }

        [JsonIgnore]
        public override bool IsPresent => Type != null && Type != TorchTypeEnum.NONE;
    }
}