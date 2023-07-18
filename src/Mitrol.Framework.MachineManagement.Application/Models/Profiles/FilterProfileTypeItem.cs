namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Classe che gestisce i sotto filtri in base al tipo di profilo
    /// </summary>
    public class FilterProfileTypeItem
    {
        public FilterProfileTypeItem(long type, string code)
        {
            ProfileType = type;
            ProfileCode = code;
            SubFilters = Enumerable.Empty<SubFilterItem>();
        }

        [JsonProperty("ProfileCode")]
        public string ProfileCode { get; set; }
        [JsonProperty("ProfileType")]
        public long ProfileType { get; set; }
        [JsonProperty("SubFilters")]
        public IEnumerable<SubFilterItem> SubFilters { get; set; }
    }

    /// <summary>
    /// Sotto Filtro 
    /// </summary>
    public class SubFilterItem
    {
        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }
        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }
        [JsonProperty("Values")]
        public IEnumerable<object> Values { get; set; }
    }

    public class SubFilterItem<T>
    {
        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }
        [JsonProperty("DisplayName")]
        public string DisplayName { get; set; }
        [JsonProperty("Values")]
        public IEnumerable<T> Values { get; set; }
    }
}
