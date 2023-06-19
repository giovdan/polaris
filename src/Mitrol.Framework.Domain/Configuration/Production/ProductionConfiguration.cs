namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;

    public class ProductionConfiguration
    {
        internal const string s_isoStartAutoJsonName = "IsoStartAuto";
        internal const string s_loadProgramsJsonName = "LoadPrograms";
        internal const string s_reserveProgramsJsonName = "ReservePrograms";
        internal const string s_processingNextProgramJsonName = "ProcessingNextProgram";
        internal const string s_handling = "Handling";

        public ProductionConfiguration()
        { }

        [JsonConstructor]
        public ProductionConfiguration([JsonProperty(s_isoStartAutoJsonName)] bool isoStartAuto,
                                     [JsonProperty(s_reserveProgramsJsonName)] bool reservePrograms,
                                     [JsonProperty(s_loadProgramsJsonName)] bool loadPrograms,
                                     [JsonProperty(s_processingNextProgramJsonName)] bool processingNextProgram,
                                     [JsonProperty(s_handling)] bool handling)
        {
            IsoStartAuto = isoStartAuto;
            LoadPrograms = loadPrograms;
            ReservePrograms = reservePrograms;
            ProcessingNextProgram = processingNextProgram;
            Handling = handling;
        }

        [JsonProperty(s_isoStartAutoJsonName)]
        public bool IsoStartAuto { get; protected set; }

        [JsonProperty(s_loadProgramsJsonName)]
        public bool LoadPrograms { get; protected set; }

        [JsonProperty(s_reserveProgramsJsonName)]
        public bool ReservePrograms { get; protected set; }

        [JsonProperty(s_processingNextProgramJsonName)]
        public bool ProcessingNextProgram { get; protected set; }

        [JsonProperty(s_handling)]
        public bool Handling { get; protected set; }
    }
}