namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class NodeReference
    {
        internal const string s_busJsonName = "Bus";
        internal const string s_nodeIdJsonName = "Id";

        public NodeReference()
        {

        }

        [JsonConstructor]
        public NodeReference([JsonProperty(s_busJsonName)] BusTypeEnum bus,
                             [JsonProperty(s_nodeIdJsonName)] int id)
        {
            Bus = bus;
            Id = id;
        }

        [JsonProperty(s_busJsonName)]
        public BusTypeEnum Bus { get; protected set; }

        [JsonProperty(s_nodeIdJsonName)]
        public int Id { get; protected set; }

        public int GetNodeId() => (Bus == BusTypeEnum.CANBUS_SYNC) ? Id + 64 : Id;
    }
}