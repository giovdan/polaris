namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public class MarkConfiguration
    {
        internal const string s_unitsJsonName = "Units";
        internal const string s_reaJetJsonProperty = "ReaJet";
       
        public MarkConfiguration() { }

        [JsonConstructor]
        public MarkConfiguration([JsonProperty(s_unitsJsonName)] List<MarkUnitConfiguration> units,
                                 [JsonProperty(s_reaJetJsonProperty)] ReaJetConfiguration reaJet)
        {
            Units = units;
            ReaJet = reaJet;
            AnyUnit = units?.Any(unit => unit.Type != MarkingUnitConfigurationEnum.None) ?? false;
        }

        [JsonIgnore]
        public bool AnyUnit { get; internal set; }

        [JsonProperty(s_unitsJsonName)]
        public IReadOnlyList<MarkUnitConfiguration> Units { get; protected set; }

        [JsonProperty(s_reaJetJsonProperty)]
        public ReaJetConfiguration ReaJet { get; protected set; }
    }
}