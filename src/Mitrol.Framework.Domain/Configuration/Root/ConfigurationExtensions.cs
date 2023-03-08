namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Mitrol.Framework.Domain.Extensions;

    public static class ConfigurationExtensions
    {
        internal const string s_errorMissingSection = "Missing mandatory configuration section.";
        internal const string s_errorMissingSetting = "Missing mandatory setting.";
        internal const string s_errorInvalidSetting = "Invalid value.";
        internal const string s_errorFileNotFoundOrInvalidPath = "A file with the specified path does not exist.";
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
    }
}
