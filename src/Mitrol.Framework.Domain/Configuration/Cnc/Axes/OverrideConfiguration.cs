namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.Linq;

    public class OverrideConfiguration
    {
        private const string s_readonlyJsonName = "ReadOnly";
        private const string s_stepsJsonName = "Steps";
        
        public OverrideConfiguration()
        {

        }

        [JsonConstructor]
        public OverrideConfiguration([JsonProperty(s_readonlyJsonName)] bool? @readonly,
                                        [JsonProperty(s_stepsJsonName)] IReadOnlyList<int> steps)
        {
            ReadOnly = @readonly;
            Steps = steps?.Distinct().OrderBy(value => value).ToArray();
        }

        [JsonProperty("Steps")] 
        public int[] Steps { get; protected set; }

        [JsonProperty(s_readonlyJsonName)]
        public bool? ReadOnly{ get; protected set; }
    }
}