namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class PlcConfiguration : PlatformConfiguration
    {
        internal const string s_ideApplicationArgsJsonName = "ApplicationArgs";
        internal const string s_ideApplicationFullPathJsonName = "ApplicationPath";
        internal const string s_plcCompilerArgsJsonName = "CompilerArgs";
        internal const string s_plcCompilerFullPathJsonName = "CompilerPath";
        internal const string s_plcProjectFilenameJsonName = "ProjectName";

        public PlcConfiguration() : base()
        {
        }

        [JsonConstructor]
        public PlcConfiguration([JsonProperty(s_logJsonName)] bool? log,
                                [JsonProperty(s_platformJsonName)] PlatformEnum? platform,
                                [JsonProperty(s_plcProjectFilenameJsonName)] string projectName,
                                [JsonProperty(s_plcCompilerFullPathJsonName)] string compilerPath,
                                [JsonProperty(s_plcCompilerArgsJsonName)] string compilerArgs,
                                [JsonProperty(s_ideApplicationFullPathJsonName)] string applicationPath,
                                [JsonProperty(s_ideApplicationArgsJsonName)] string applicationArgs)
            : base(log, platform)
        {
            ProjectName = projectName;
            CompilerPath = compilerPath;
            CompilerArgs = compilerArgs;
            ApplicationPath = applicationPath;
            ApplicationArgs = applicationArgs;
        }

        [JsonProperty(s_ideApplicationArgsJsonName)]
        public string ApplicationArgs { get; protected set; }

        [JsonProperty(s_ideApplicationFullPathJsonName)]
        public string ApplicationPath { get; protected set; }

        [JsonProperty(s_plcCompilerArgsJsonName)]
        public string CompilerArgs { get; protected set; }

        [JsonProperty(s_plcCompilerFullPathJsonName)]
        public string CompilerPath { get; protected set; }

        [JsonProperty(s_plcProjectFilenameJsonName)]
        public string ProjectName { get; protected set; }
    }
}