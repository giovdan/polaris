namespace Mitrol.Framework.Domain.Configuration
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class NotchConfiguration
    {
        internal const string s_typeJsonName = "Type";
        
        [JsonConstructor]
        public NotchConfiguration([JsonProperty(s_typeJsonName)] StozzaTypeEnum type)
        {
            Type = type;
        }


        [JsonProperty(s_typeJsonName)]
        public StozzaTypeEnum Type { get; protected set; }

        [JsonIgnore]
        public bool IsPresent => Type != StozzaTypeEnum.None;
    }
}
