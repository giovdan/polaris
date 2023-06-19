namespace Mitrol.Framework.Domain.Models
{
    using Newtonsoft.Json;

    public class CauseSolutionPair
    {
        public CauseSolutionPair()
        {

        }

        public CauseSolutionPair(string causeLocalizationKey, string solutionLocalizationKey)
        {
            CauseLocalizationKey = causeLocalizationKey;
            SolutionLocalizationKey = solutionLocalizationKey;
        }

        [JsonProperty("CauseLocalizationKey")]
        public string CauseLocalizationKey { get; set; }

        [JsonProperty("SolutionLocalizationKey")]
        public string SolutionLocalizationKey { get; set; }
    }
}