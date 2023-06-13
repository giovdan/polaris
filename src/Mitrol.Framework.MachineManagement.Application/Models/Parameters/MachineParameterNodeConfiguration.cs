namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class MachineParameterNodeConfiguration
    {
        [JsonProperty("NodeName")]
        public string NodeName { get; set; }
        [JsonProperty("Priority")]
        public short Priority { get; set; }
        [JsonProperty("Groups")]
        public List<MachineParameterGroupConfiguration> Groups { get; set; }
    }
}
