namespace Mitrol.Framework.Domain.Configuration.Extensions
{
    using Mitrol.Framework.Domain.Configuration.Enums;
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Extensions;

    public static class ConfigurationExtensions
    {
        internal const string s_errorMissingSection = "Missing mandatory configuration section";
        internal const string s_errorMissingSetting = "Missing mandatory setting";
        internal const string s_errorInvalidSetting = "Invalid value";
        internal const string s_errorFileNotFoundOrInvalidPath = "A file with the specified path does not exist";
        internal const string s_errorDuplicates = "Duplicates found: {duplicates}";

        /// <summary>
        /// Recupera il Processing Technology dal tipo di Plasma
        /// </summary>
        public static ProcessingTechnologyEnum GetProcessingTechnology(this PlaConfiguration plasmaConfiguration)
        {
            switch (plasmaConfiguration?.Type)
            {
                case PlasmaTypeEnum.HPR:
                case PlasmaTypeEnum.HPRA:
                case PlasmaTypeEnum.HSD:
                case PlasmaTypeEnum.POWERMAX:
                    return ProcessingTechnologyEnum.PlasmaHPR;
                case PlasmaTypeEnum.XPR:
                    return ProcessingTechnologyEnum.PlasmaXPR;
                default:
                    return ProcessingTechnologyEnum.Default;
            }
        }

        public static bool IsConfigured(this DrillConfiguration source) => source?.AnyUnit ?? false;
        public static bool IsConfigured(this PlaConfiguration source) => source?.AnyUnit ?? false;
        public static bool IsConfigured(this OxyConfiguration source) => source?.AnyUnit ?? false;
        public static bool IsConfigured(this SawConfiguration source) => source?.AnyUnit ?? false;
        public static bool IsConfigured(this MarkConfiguration source) => source?.AnyUnit ?? false;

        public static bool IsConfigured(this ReaJetConfiguration source) => source?.IpAddress.IsNotNullOrEmpty() ?? false;

        /// <summary>
        /// Controlla il tipo di macchina
        /// </summary>
        /// <returns>Restituisce true se il tipo della macchina è relativa al MachineEnum</returns>
        public static bool IsTypeOf(this IRootConfiguration configuration, MachineEnum machineType)
        {
            switch (machineType)
            {
                case MachineEnum.FORA: return configuration.Machine.Type is MachineTypeEnum.TANG_BASE;
                case MachineEnum.PIASTRINA: return configuration.Machine.Type is MachineTypeEnum.TANG_PIASTRINA;
                case MachineEnum.CALIBRO: return configuration.Machine.Type is MachineTypeEnum.TANG_CALIBRO;
                case MachineEnum.F401P: return configuration.Machine.Type is MachineTypeEnum.TANG_F401P;
                case MachineEnum.P306PS: return configuration.Machine.Type is MachineTypeEnum.TANG_306PS;
                case MachineEnum.P504PS: return configuration.Machine.Type is MachineTypeEnum.TANG_504PS;
                case MachineEnum.P504_PLA: return configuration.Machine.Type is MachineTypeEnum.TANG_504PS && configuration.Setup.Pla.AnyUnit;
                case MachineEnum.V11: return configuration.Machine.Type is MachineTypeEnum.TANG_V11;
                case MachineEnum.E12: return configuration.Machine.Type is MachineTypeEnum.TANG_V11;
                case MachineEnum.A166: return configuration.Machine.Type is MachineTypeEnum.TANG_A166;
                case MachineEnum.A166_TRI: return configuration.Machine.Type is MachineTypeEnum.TANG_A166 && (configuration.Setup.Drill.AngularUnitType is ForAngEnum.UF11_22_D || configuration.Setup.Drill.AngularUnitType is ForAngEnum.UF11_22_P || configuration.Setup.Drill.AngularUnitType is ForAngEnum.UF11_22_M);
                case MachineEnum.A166_TRI_P: return configuration.Machine.Type is MachineTypeEnum.TANG_A166 && configuration.Setup.Drill.AngularUnitType is ForAngEnum.UF11_22_P;
                case MachineEnum.A166_TRI_D: return configuration.Machine.Type is MachineTypeEnum.TANG_A166 && configuration.Setup.Drill.AngularUnitType is ForAngEnum.UF11_22_D;
                case MachineEnum.A166_TRI_M: return configuration.Machine.Type is MachineTypeEnum.TANG_A166 && configuration.Setup.Drill.AngularUnitType is ForAngEnum.UF11_22_M;
                case MachineEnum.A166M: return configuration.Machine.Type is MachineTypeEnum.TANG_A166 && configuration.Machine.CarriageType is CarriageConfigurationEnum.PZ;
                case MachineEnum.A166R: return configuration.Machine.Type is MachineTypeEnum.TANG_A166 && configuration.Machine.CarriageType is CarriageConfigurationEnum.RULLI;
                case MachineEnum.TIPOC: return configuration.Machine.Type is MachineTypeEnum.TANG_TIPOC;
                case MachineEnum.TIPOA31: return configuration.Machine.Type is MachineTypeEnum.TANG_TIPOA31;
                case MachineEnum.GEMINI: return configuration.Machine.Type is MachineTypeEnum.TANG_GEMINI && configuration.Cnc.Fanuc.IsPresent;
                case MachineEnum.TIPOB254: return configuration.Machine.Type is MachineTypeEnum.TANG_TIPOB254;
                case MachineEnum.GANTRY_T: return configuration.Machine.Type is MachineTypeEnum.TANG_GANTRY_T;
                case MachineEnum.TIPOALG: return configuration.Machine.Type is MachineTypeEnum.TANG_TIPOALG;
                case MachineEnum.ENDEAVOUR: return configuration.Machine.Type is MachineTypeEnum.TANG_ENDEAVOUR;
                case MachineEnum.ORIENT: return configuration.Machine.Type is MachineTypeEnum.TANG_ENDEAVOUR && configuration.Setup.Drill.IsOrient(UnitEnum.B);
                case MachineEnum.ORIENT_1T: return configuration.Machine.Type is MachineTypeEnum.TANG_ENDEAVOUR && configuration.Setup.Drill.IsOrient(UnitEnum.B) && configuration.Setup.Drill.HasSlots(UnitEnum.A) is false;
                case MachineEnum.ORIENT_2T: return configuration.Machine.Type is MachineTypeEnum.TANG_ENDEAVOUR && configuration.Setup.Drill.IsOrient(UnitEnum.B) && configuration.Setup.Drill.HasSlots(UnitEnum.A);
                case MachineEnum.HP_S: return configuration.Machine.Type is MachineTypeEnum.TANG_HP_S;
                case MachineEnum.TIPOG: return configuration.Machine.Type is MachineTypeEnum.TANG_TIPOG;
                case MachineEnum.TIPOG_DX: return configuration.Machine.Type is MachineTypeEnum.TANG_TIPOG && configuration.Machine.FixedWire is FixedWireEnum.D0;
                case MachineEnum.TIPOG_SX: return configuration.Machine.Type is MachineTypeEnum.TANG_TIPOG && configuration.Machine.FixedWire is FixedWireEnum.D1;
                case MachineEnum.GANTRY_T_O: return configuration.Machine.Type is MachineTypeEnum.TANG_GANTRY_T_O;
                case MachineEnum.GEMINIFF: return configuration.Machine.Type is MachineTypeEnum.TANG_GEMINI && configuration.Cnc.Fanuc.IsPresent is false;
                case MachineEnum.FORATRICI: return configuration.Machine.Type is MachineTypeEnum.TANG_BASE || configuration.Machine.Type is MachineTypeEnum.TANG_V11 || configuration.Machine.Type is MachineTypeEnum.TANG_GANTRY_T || configuration.Machine.Type is MachineTypeEnum.TANG_ENDEAVOUR;
                case MachineEnum.FAM_ANGOLARI: return configuration.Machine.Type is MachineTypeEnum.TANG_A166 || configuration.Machine.Type is MachineTypeEnum.TANG_HP_S;
                case MachineEnum.FAM_PIASTRE: return configuration.Machine.Type is MachineTypeEnum.TANG_PIASTRINA || configuration.Machine.Type is MachineTypeEnum.TANG_F401P;
                case MachineEnum.ROB_FANUC: return configuration.Setup.Robot.IsPresent && configuration.Cnc.Fanuc.IsPresent;
                case MachineEnum.CNC_FANUC: return (configuration.Machine.Type is MachineTypeEnum.TANG_GEMINI || configuration.Machine.Type is MachineTypeEnum.TANG_TIPOG) && configuration.Cnc.Fanuc.IsPresent;
                default: return false;
            };
        }
    }
}
