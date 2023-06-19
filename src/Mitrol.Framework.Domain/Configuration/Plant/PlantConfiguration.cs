namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;

    public class PlantConfiguration
    {
        internal const string s_nameJsonProperty = "Name";

        public PlantConfiguration() { }

        /// <summary>
        /// Json constructor that allows de-serialization of read-only properties
        /// </summary>
        [JsonConstructor]
        public PlantConfiguration([JsonProperty(s_nameJsonProperty)] string name)
        {
            Name = name;
        }

        [JsonProperty(s_nameJsonProperty)]
        public string Name { get; protected set; }
    }
}