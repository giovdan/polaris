namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Configuration.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public class DrillConfiguration
    {
        internal const string s_forAngJsonName = "AngularUnitType";
        internal const string s_unitsJsonName = "Units";
        internal const string s_scribingBackWeb = "ScribingBackWeb";

        public DrillConfiguration() { }

        [JsonConstructor]
        public DrillConfiguration([JsonProperty(s_unitsJsonName)] List<DrillUnitConfiguration> units,
                                  [JsonProperty(s_forAngJsonName)] ForAngEnum? angularUnitType,
                                  [JsonProperty(s_scribingBackWeb)] string scribingBackWeb)
        {
            Units = units;
            AngularUnitType = angularUnitType;
            ScribingBackWeb = scribingBackWeb;
        }

        [JsonIgnore]
        public bool AnyUnit => Units?.Any(unit => unit.IsPresent) ?? false;

        [JsonProperty(s_forAngJsonName)]
        public ForAngEnum? AngularUnitType { get; protected set; }

        [JsonProperty(s_scribingBackWeb)]
        public string ScribingBackWeb { get; protected set; }

        [JsonProperty(s_unitsJsonName)]
        public IReadOnlyList<DrillUnitConfiguration> Units { get; protected set; }

        public bool IsOrient(UnitEnum unit) => Units.SingleOrDefault(u => u.Id == unit)?.Orient ?? false;

        public bool HasSlots(UnitEnum unit) => Units.SingleOrDefault(u => u.Id == unit)?.SlotCount > 0;
    }
}