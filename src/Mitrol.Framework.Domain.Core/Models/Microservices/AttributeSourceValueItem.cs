using Newtonsoft.Json;

namespace Mitrol.Framework.Domain.Models
{
    public class AttributeSourceValueItem
    {
        [JsonProperty("Value")]
        public object Value { get; set; }
        //codice dell'elemento che deve essere tradotto prima di essere visualizzato.
        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }
        [JsonIgnore()]
        public bool IsDynamicType { get; set; }
        //string da valorizzare nel caso non si voglia far tradurre il codice dell'elemento da visualizzare.
        [JsonProperty("LocalizedText")]
        public string LocalizedText { get; set; }

        public AttributeSourceValueItem()
        {
            LocalizedText = string.Empty;
        }
    }
}
