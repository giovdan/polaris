namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class that represents the settings for Programming configuration.
    /// </summary>
    public class ProgrammingConfiguration
    {
        internal const string s_macroCopeJsonProperty = "MacroCope";
        internal const string s_macroCutJsonProperty = "MacroCut";
        internal const string s_macroMillJsonProperty = "MacroMill";
        internal const string s_plasmaJsonName = "Plasma";
        internal const string s_plasmaMarkingFontPathJsonName = "PlasmaMarkingFontPath";
        internal const string s_reaJetFixedHeadJsonName = "ReaJetFixedHead";
        internal const string s_reaJetJsonName = "ReaJet";
        internal const string s_scribingJsonName = "Scribing";
        internal const string s_scribingMarkingFontPathJsonName = "ScribingMarkingFontPath";

        /// <summary>
        /// Default constructor required for AutoMapper mappings.
        /// </summary>
        public ProgrammingConfiguration()
        { }

        /// <summary>
        /// Json constructor that allows de-serialization of read-only properties.
        /// </summary>
        [JsonConstructor]
        public ProgrammingConfiguration([JsonProperty(s_macroCutJsonProperty)] MacroTypeConfiguration macroCut,
                                    [JsonProperty(s_macroMillJsonProperty)] MacroTypeConfiguration macroMill,
                                    [JsonProperty(s_macroCopeJsonProperty)] MacroTypeConfiguration macroCope,
                                    [JsonProperty(s_reaJetFixedHeadJsonName)] FontDefinitionConfiguration reaJetF,
                                    [JsonProperty(s_reaJetJsonName)] FontDefinitionConfiguration reaJet,
                                    [JsonProperty(s_plasmaJsonName)] FontDefinitionConfiguration plasma,
                                    [JsonProperty(s_scribingJsonName)] FontDefinitionConfiguration scribing,
                                    [JsonProperty(s_scribingMarkingFontPathJsonName)] int? scribingMarkingFontPath,
                                    [JsonProperty(s_plasmaMarkingFontPathJsonName)] int? plasmaMarkingFontPath)
        {
            MacroCut = macroCut;
            MacroMill = macroMill;
            MacroCope = macroCope;
            ReaJetF = reaJetF;
            ReaJet = reaJet;
            Plasma = plasma;
            Scribing = scribing;
            ScribingMarkingFontPath = scribingMarkingFontPath;
            PlasmaMarkingFontPath = plasmaMarkingFontPath;
        }

        [JsonProperty(s_macroCopeJsonProperty)]
        public MacroTypeConfiguration MacroCope { get; protected set; }

        [JsonProperty(s_macroCutJsonProperty)]
        public MacroTypeConfiguration MacroCut { get; protected set; }

        [JsonProperty(s_macroMillJsonProperty)]
        public MacroTypeConfiguration MacroMill { get; protected set; }

        [JsonProperty(s_plasmaJsonName)]
        public FontDefinitionConfiguration Plasma { get; protected set; }

        [JsonProperty(s_plasmaMarkingFontPathJsonName)]
        public int? PlasmaMarkingFontPath { get; protected set; }

        [JsonProperty(s_reaJetJsonName)]
        public FontDefinitionConfiguration ReaJet { get; protected set; }

        [JsonProperty(s_reaJetFixedHeadJsonName)]
        public FontDefinitionConfiguration ReaJetF { get; protected set; }

        [JsonProperty(s_scribingJsonName)]
        public FontDefinitionConfiguration Scribing { get; protected set; }

        [JsonProperty(s_scribingMarkingFontPathJsonName)]
        public int? ScribingMarkingFontPath { get; protected set; }
    }
}