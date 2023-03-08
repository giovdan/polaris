namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public class OxyConfiguration
    {
        internal const string s_automaticJsonName = "Auto";
        internal const string s_torchesConfigurationJsonName = "Torches";

        public OxyConfiguration()
        { }

        [JsonConstructor]
        public OxyConfiguration([JsonProperty(s_torchesConfigurationJsonName)] IReadOnlyList<TorchUnitConfiguration> torches,
                                [JsonProperty(s_automaticJsonName)] bool auto)
        {
            Torches = torches?.Where(torch => torch.IsPresent).ToList();
            Auto = auto;
        }

        [JsonIgnore]
        public bool AnyUnit => Torches?.Any(torch => torch.IsPresent) ?? false;

        [JsonProperty(s_automaticJsonName)]
        public bool Auto { get; protected set; }

        [JsonProperty(s_torchesConfigurationJsonName)]
        public IReadOnlyList<TorchUnitConfiguration> Torches { get; protected set; }
    }
}