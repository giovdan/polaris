namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class EtherCATNodeConfiguration
    {
        private const string s_enableJsonName = "Enable";
        private const string s_indexJsonName = "Index";
        private const string s_typeJsonName = "Type";

        [JsonConstructor]
        public EtherCATNodeConfiguration([JsonProperty(s_enableJsonName)] bool enable,
                                         [JsonProperty(s_indexJsonName)] byte index,
                                         [JsonProperty(s_typeJsonName)] EtherCATTypeEnum type)
        {
            Enable = enable;
            Index = index;
            Type = type;
        }

        [JsonProperty(s_enableJsonName)]
        public bool Enable { get; protected set; }

        [JsonProperty(s_indexJsonName)]
        public byte Index { get; protected set; }

        [JsonProperty(s_typeJsonName)]
        public EtherCATTypeEnum Type { get; protected set; }
    }
}