namespace Mitrol.Framework.Domain.Configuration.License
{
    using Newtonsoft.Json;

    public class LicenseConfiguration
    {
        internal const string s_codeJsonProperty = "Code";
        internal const string s_idJsonProperty = "Id";

        public LicenseConfiguration() { }

        /// <summary>
        /// Json constructor that allows de-serialization of read-only properties
        /// </summary>
        [JsonConstructor]
        public LicenseConfiguration([JsonProperty(s_codeJsonProperty)] string code,
                                    [JsonProperty(s_idJsonProperty)] string id)
        {
            Code = code;
            Id = id;
        }

        [JsonProperty(s_codeJsonProperty)]
        public string Code { get; protected set; }

        [JsonProperty(s_idJsonProperty)]
        public string Id { get; protected set; }
    }
}
