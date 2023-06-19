namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Configuration.Models;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Macro;
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Class that represents the settings for Machine configuration.
    /// </summary>
    public partial class MachineConfiguration : PlatformConfiguration
    {
        internal const string s_carriageConfigurationJsonName = "CarriageType";
        internal const string s_consolesJsonProperty = "Consoles";
        internal const string s_doubleSlideJsonName = "DoubleSlide";
        internal const string s_fixedWireJsonName = "FixedWire";
        internal const string s_gantryModeJsonName = "GantryMode";
        internal const string s_operationConfigurationJsonName = "Operations";
        internal const string s_orderingTypeJsonName = "OrderingType";
        internal const string s_probeCodesConfigurationJsonName = "ProbeCodes";
        internal const string s_profilesConfigurationJsonName = "Profiles";
        internal const string s_programTypeConfigurationJsonName = "ProgramTypes";
        internal const string s_skippedTypesConfigurationJsonName = "SkippedToolTypes";
        internal const string s_toolTypesConfigurationJsonName = "ToolTypes";
        internal const string s_typeJsonName = "Type";

        /// <summary>
        /// Default constructor required for AutoMapper mappings.
        /// </summary>
        public MachineConfiguration()
        { }

        /// <summary>
        /// Json constructor that allows de-serialization of read-only properties.
        /// </summary>
        [JsonConstructor]
        public MachineConfiguration([JsonProperty(s_logJsonName)] bool? log,
                                    [JsonProperty(s_platformJsonName)] PlatformEnum? platform,
                                    [JsonProperty(s_typeJsonName)] MachineTypeEnum? type,
                                    [JsonProperty(s_fixedWireJsonName)] FixedWireEnum? fixedWire,
                                    [JsonProperty(s_orderingTypeJsonName)] OrderTypeConfigurationEnum? orderingType,
                                    [JsonProperty(s_carriageConfigurationJsonName)] CarriageConfigurationEnum? carriageType,
                                    [JsonProperty(s_gantryModeJsonName)] TipoGantryEnum? gantryMode,
                                    [JsonProperty(s_doubleSlideJsonName)] bool? doubleSlide,
                                    [JsonProperty(s_toolTypesConfigurationJsonName)] FlagsDictionary<ToolTypeEnum> toolTypes,
                                    [JsonProperty(s_skippedTypesConfigurationJsonName)] FlagsDictionary<ToolTypeEnum> skippedToolTypes,
                                    [JsonProperty(s_profilesConfigurationJsonName)] FlagsDictionary<ProfileTypeEnum> profiles,
                                    [JsonProperty(s_operationConfigurationJsonName)] FlagsDictionary<OperationTypeEnum> operations,
                                    [JsonProperty(s_probeCodesConfigurationJsonName)] FlagsDictionary<ProbeCodeEnum> probeCodes,
                                    [JsonProperty(s_programTypeConfigurationJsonName)] FlagsDictionary<ProgramTypeEnum> programTypes,
                                    [JsonProperty(s_consolesJsonProperty)] IEnumerable<ConsoleConfiguration> consoles)
            : base(log, platform)
        {
            Type = type;
            FixedWire = fixedWire;
            OrderingType = orderingType;
            ToolTypes = toolTypes;
            SkippedToolTypes = skippedToolTypes;
            Profiles = profiles;
            Operations = operations;
            ProbeCodes = probeCodes;
            CarriageType = carriageType;
            DoubleSlide = doubleSlide;
            GantryMode = gantryMode;
            ProgramTypes = programTypes;
            Consoles = consoles?.ToList();
        }

        [JsonIgnore]
        public bool AnyOperation => Operations.Any(operation => operation.Value);

        [JsonIgnore]
        public bool AnyProbeCode => ProbeCodes.Any(probeCode => probeCode.Value);

        [JsonIgnore]
        public bool AnyProfile => Profiles.Any(profile => profile.Value);

        [JsonIgnore]
        public bool AnyProgramType => ProgramTypes.Any(programType => programType.Value);

        [JsonIgnore]
        public bool AnyToolType => ToolTypes.Any(toolType => toolType.Value);

        [JsonProperty(s_consolesJsonProperty)]
        public IReadOnlyList<ConsoleConfiguration> Consoles { get; protected set; }

        [JsonProperty(s_doubleSlideJsonName)]
        public bool? DoubleSlide { get; protected set; }

        [JsonProperty(s_fixedWireJsonName)]
        public FixedWireEnum? FixedWire { get; protected set; }

        [JsonProperty(s_gantryModeJsonName)]
        public TipoGantryEnum? GantryMode { get; protected set; }

        [JsonProperty(s_operationConfigurationJsonName)]
        public IReadOnlyDictionary<OperationTypeEnum, bool> Operations { get; protected set; }

        [JsonProperty(s_probeCodesConfigurationJsonName)]
        public IReadOnlyDictionary<ProbeCodeEnum, bool> ProbeCodes { get; protected set; }

        [JsonProperty(s_profilesConfigurationJsonName)]
        public IReadOnlyDictionary<ProfileTypeEnum, bool> Profiles { get; protected set; }

        [JsonProperty(s_programTypeConfigurationJsonName)]
        public IReadOnlyDictionary<ProgramTypeEnum, bool> ProgramTypes { get; protected set; }

        [JsonProperty(s_toolTypesConfigurationJsonName)]
        public IReadOnlyDictionary<ToolTypeEnum, bool> ToolTypes { get; protected set; }

        [JsonProperty(s_orderingTypeJsonName)]
        public OrderTypeConfigurationEnum? OrderingType { get; protected set; }

        [JsonProperty(s_skippedTypesConfigurationJsonName)]
        public IReadOnlyDictionary<ToolTypeEnum, bool> SkippedToolTypes { get; protected set; }

        [JsonProperty(s_carriageConfigurationJsonName)]
        public CarriageConfigurationEnum? CarriageType { get; protected set; }

        [JsonProperty(s_typeJsonName)]
        public MachineTypeEnum? Type { get; protected set; }

        public bool IsMacroSupported(MacroTypeEnum macroType)
        {
            switch (macroType)
            {
                // se è abilitata l'Operazione "MCut" allora considero abilitate le macro di taglio
                case MacroTypeEnum.MacroCut when Operations[OperationTypeEnum.MCut]:
                //se è abilitata l'Operazione "Mill" allora considero abilitate le macro di fresatura
                case MacroTypeEnum.MacroMill when Operations[OperationTypeEnum.Mill]:
                //se è abilitata l'Operazione "Cope" allora considero abilitate le macro Robot
                case MacroTypeEnum.MacroRobot when Operations[OperationTypeEnum.Cope]:
                    return true;

                default:
                    return false;
            }
        }
    }
}