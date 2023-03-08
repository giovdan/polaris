namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class MarkUnitConfiguration : UnitConfiguration
    {
        internal const string s_markUnitTypeJsonName = "Type";
        internal const string s_markingSideJsonName = "Side";
        internal const string s_markingReverseJsonName = "Reverse";
        internal const string s_characterPositionsJsonName = "CharacterPositionOnDisk";

        public MarkUnitConfiguration()
        {
        }

        public MarkUnitConfiguration([JsonProperty(s_unitJsonName)] UnitEnum? unitId,
                                     [JsonProperty(s_gridPositionJsonName)] GridPosition gridPosition,
                                     [JsonProperty(s_markUnitTypeJsonName)] MarkingUnitConfigurationEnum? type,
                                     [JsonProperty(s_markingReverseJsonName)] bool? reverse,
                                     [JsonProperty(s_characterPositionsJsonName)] Dictionary<char, byte> characterPositionOnDisk)
            : base(unitId, gridPosition)
        {
            Type = type;
            Reverse = reverse;
            CharacterPositionOnDisk = characterPositionOnDisk;
        }

        public override bool IsPresent => Type != null && Type != MarkingUnitConfigurationEnum.None;

        [JsonProperty(s_markUnitTypeJsonName)]
        public MarkingUnitConfigurationEnum? Type { get; protected set; }

        [JsonProperty(s_markingReverseJsonName)]
        public bool? Reverse { get; protected set; }

        [JsonProperty(s_characterPositionsJsonName)]
        public IReadOnlyDictionary<char, byte> CharacterPositionOnDisk { get; protected set; }

    }
}