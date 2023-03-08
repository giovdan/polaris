namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class EtherCATConfiguration
    {
        private const string s_nodesJsonName = "Nodes";

        public EtherCATConfiguration()
        { }

        [JsonConstructor]
        public EtherCATConfiguration([JsonProperty(s_nodesJsonName)] IReadOnlyList<EtherCATNodeConfiguration> nodes)
        {
            Nodes = nodes;
        }

        [JsonProperty(s_nodesJsonName)]
        public IReadOnlyList<EtherCATNodeConfiguration> Nodes { get; protected set; }
    }
}