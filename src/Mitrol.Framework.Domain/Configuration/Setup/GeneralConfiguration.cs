namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;

    public class GeneralConfiguration
    {
        internal const string s_profileAutomaticConfirmJsonName = "ProfileAutomaticConfirm";
        internal const string s_profileAutomaticConfirmTypeExcludedJsonName = "ProfileAutomaticConfirmTypeExcluded";

        internal const string s_toleranceProfileLengthJsonName = "ToleranceProfileLength";
        internal const string s_toleranceProfileWidthJsonName = "ToleranceProfileWidth";
        internal const string s_toleranceProfileHeightJsonName = "ToleranceProfileHeight";
        internal const string s_toleranceProfileThicknessJsonName = "ToleranceProfileThickness";

        public GeneralConfiguration() { }

        [JsonConstructor]
        public GeneralConfiguration([JsonProperty(s_profileAutomaticConfirmJsonName)] bool? profileAutomaticConfirm,
                                    [JsonProperty(s_profileAutomaticConfirmTypeExcludedJsonName)] bool? profileAutomaticConfirmTypeExcluded,
                                    [JsonProperty(s_toleranceProfileLengthJsonName)] ToleranceConfiguration toleranceProfileLength,
                                    [JsonProperty(s_toleranceProfileWidthJsonName)] ToleranceConfiguration toleranceProfileWidth,
                                    [JsonProperty(s_toleranceProfileHeightJsonName)] ToleranceConfiguration toleranceProfileHeight,
                                    [JsonProperty(s_toleranceProfileThicknessJsonName)] ToleranceConfiguration toleranceProfileThickness)
        {
            ProfileAutomaticConfirm = profileAutomaticConfirm;
            ProfileAutomaticConfirmTypeExcluded = profileAutomaticConfirmTypeExcluded;
            ToleranceProfileLength = toleranceProfileLength;
            ToleranceProfileWidth = toleranceProfileWidth;
            ToleranceProfileHeight = toleranceProfileHeight;
            ToleranceProfileThickness = toleranceProfileThickness;
        }

        [JsonProperty(s_profileAutomaticConfirmJsonName)]
        public bool? ProfileAutomaticConfirm { get; protected set; }

        [JsonProperty(s_profileAutomaticConfirmTypeExcludedJsonName)]
        public bool? ProfileAutomaticConfirmTypeExcluded { get; protected set; }

        [JsonProperty(s_toleranceProfileLengthJsonName)]
        public ToleranceConfiguration ToleranceProfileLength { get; protected set; }

        [JsonProperty(s_toleranceProfileWidthJsonName)]
        public ToleranceConfiguration ToleranceProfileWidth { get; protected set; }

        [JsonProperty(s_toleranceProfileHeightJsonName)]
        public ToleranceConfiguration ToleranceProfileHeight { get; protected set; }

        [JsonProperty(s_toleranceProfileThicknessJsonName)]
        public ToleranceConfiguration ToleranceProfileThickness { get; protected set; }
    }
}
