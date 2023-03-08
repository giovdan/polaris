namespace Mitrol.Framework.Domain.Configuration
{
    using Newtonsoft.Json;

    public class GridPosition
    {
        internal const string s_xJsonName = "X";
        internal const string s_yJsonName = "Y";

        public GridPosition() { }

        [JsonConstructor]
        public GridPosition([JsonProperty(s_xJsonName)] float x,
                            [JsonProperty(s_yJsonName)] float y)
        {
            X = x;
            Y = y;
        }

        [JsonProperty(s_xJsonName)]
        public float X { get; protected set; }

        [JsonProperty(s_yJsonName)]
        public float Y { get; protected set; }
    }
}