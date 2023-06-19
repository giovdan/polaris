namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public class SawConfiguration
    {
        internal const string s_unitsJsonName = "Units";

        public SawConfiguration()
        { }

        [JsonConstructor]
        public SawConfiguration([JsonProperty(s_unitsJsonName)] IReadOnlyList<SawUnitConfiguration> units)
        {
            Units = units;
        }

        [JsonIgnore]
        public bool AnyUnit => Units?.Any(unit => unit.IsPresent) ?? false;

        [JsonProperty(s_unitsJsonName)]
        public IReadOnlyList<SawUnitConfiguration> Units { get; protected set; }
    }
}
