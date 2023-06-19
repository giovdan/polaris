namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public class PlaConfiguration : LogConfiguration
    {
        internal const string s_consoleXprJsonName = "ConsoleXprType";
        internal const string s_earlyParametersSendJsonName = "EarlyParameterSend";
        internal const string s_PlasmaTypeJsonName = "Type";
        internal const string s_torchesConfigurationJsonName = "Torches";
        internal const string s_trueHoleModalityJsonName = "TrueHoleModality";
        
        public PlaConfiguration() { }

        [JsonConstructor]
        public PlaConfiguration([JsonProperty(s_logJsonName)] bool? log,
                                [JsonProperty(s_torchesConfigurationJsonName)] IReadOnlyList<TorchUnitConfiguration> torches,
                                [JsonProperty(s_earlyParametersSendJsonName)] bool? earlyParameterSend,
                                [JsonProperty(s_PlasmaTypeJsonName)] PlasmaTypeEnum? type,
                                [JsonProperty(s_consoleXprJsonName)] MinRequiredConsoleTypeEnum? consoleXprType,
                                [JsonProperty(s_trueHoleModalityJsonName)] byte? trueHoleModality)
            : base(log)
        {
            Torches = torches;
            EarlyParametersSend = earlyParameterSend;
            Type = type;
            ConsoleXprType = consoleXprType;
            TrueHoleModality = trueHoleModality;
        }

        [JsonProperty(s_earlyParametersSendJsonName)]
        public bool? EarlyParametersSend { get; protected set; }

        [JsonIgnore]
        public bool AnyUnit => Torches?.Any(torch => torch.IsPresent) ?? false;

        [JsonProperty(s_consoleXprJsonName)]
        public MinRequiredConsoleTypeEnum? ConsoleXprType { get; protected set; }

        [JsonProperty(s_trueHoleModalityJsonName)]
        public byte? TrueHoleModality { get; protected set; }

        [JsonProperty(s_torchesConfigurationJsonName)]
        public IReadOnlyList<TorchUnitConfiguration> Torches { get; protected set; }

        [JsonProperty(s_PlasmaTypeJsonName)]
        public PlasmaTypeEnum? Type { get; protected set; }
    }
}