namespace Mitrol.Framework.Domain.Extensions
{
    using Newtonsoft.Json.Converters;
    using Newtonsoft.Json.Linq;
    using System.Collections.Generic;

    public static class JsonExtensions
    {
        public static IEnumerable<(string propPath, string value)> Flatten(this JToken node)
        {
            if (node.Type == JTokenType.Object)
            {
                foreach (JProperty child in node.Children<JProperty>())
                {
                    foreach (var item in Flatten(child.Value))
                    {
                        yield return item;
                    }
                }
            }
            else if (node.Type == JTokenType.Array)
            {
                foreach (JToken child in node.Children())
                {
                    foreach (var item in Flatten(child))
                    {
                        yield return item;
                    }
                }
            }
            else
            {
                yield return (node.Path, node.ToString());
            }
        }
    }
}
