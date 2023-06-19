namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;

    /// <summary>
    /// Class that represents the settings for Setup configuration.
    /// </summary>
    public class SetupConfiguration
    {
        internal const string s_drillJsonName = "Drill";
        internal const string s_generalJsonName = "General";
        internal const string s_markJsonProperty = "Mark";
        internal const string s_notchJsonProperty = "Notch";
        internal const string s_oxyJsonName = "Oxy";
        internal const string s_plaJsonName = "Pla";
        internal const string s_robotJsonProperty = "Robot";
        internal const string s_sawJsonName = "Saw";

        /// <summary>
        /// Default constructor required for AutoMapper mappings.
        /// </summary>
        public SetupConfiguration()
        {
        }

        /// <summary>
        /// Json constructor that allows de-serialization of read-only properties.
        /// </summary>
        [JsonConstructor]
        public SetupConfiguration([JsonProperty(s_drillJsonName)] DrillConfiguration drill,
                                  [JsonProperty(s_plaJsonName)] PlaConfiguration pla,
                                  [JsonProperty(s_oxyJsonName)] OxyConfiguration oxy,
                                  [JsonProperty(s_sawJsonName)] SawConfiguration saw,
                                  [JsonProperty(s_generalJsonName)] GeneralConfiguration general,
                                  [JsonProperty(s_notchJsonProperty)] NotchConfiguration notch,
                                  [JsonProperty(s_markJsonProperty)] MarkConfiguration mark,
                                  [JsonProperty(s_robotJsonProperty)] RobotConfiguration robot)
        {
            Drill = drill;
            Pla = pla;
            Oxy = oxy;
            Saw = saw;
            General = general;
            Notch = notch;
            Mark = mark;
            Robot = robot;
        }

        [JsonProperty(s_drillJsonName)]
        public DrillConfiguration Drill { get; protected set; }

        [JsonProperty(s_generalJsonName)]
        public GeneralConfiguration General { get; protected set; }

        [JsonProperty(s_markJsonProperty)]
        public MarkConfiguration Mark { get; protected set; }

        [JsonProperty(s_notchJsonProperty)]
        public NotchConfiguration Notch { get; protected set; }

        [JsonProperty(s_oxyJsonName)]
        public OxyConfiguration Oxy { get; protected set; }

        [JsonProperty(s_plaJsonName)]
        public PlaConfiguration Pla { get; protected set; }

        [JsonProperty(s_robotJsonProperty)]
        public RobotConfiguration Robot { get; protected set; }

        [JsonProperty(s_sawJsonName)]
        public SawConfiguration Saw { get; protected set; }
    }
    
}