namespace Mitrol.Framework.Domain.Models
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;

    public class CountGroupItem
    {
        [JsonProperty("GroupName")]
        public long GroupName { get; set; }
        [JsonProperty("Count")]
        public int Count { get; set; }
    }

    public class BaseGroupItem<T>
    {
        public BaseGroupItem()
        {
            Details = Array.Empty<T>();
        }

        public BaseGroupItem(BaseGroupItem<T> group) : this()
        {
            this.LocalizationKey = group.LocalizationKey;
            Hidden = false;
        }

        [JsonProperty("Priority")]
        public int Priority { get; set; }

        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }

        [JsonProperty("Details")]
        public IEnumerable<T> Details { get; set; }

        [JsonProperty("Hidden")]
        public bool Hidden { get; set; }
    }

    public class BaseGroupExtItem<T>: BaseGroupItem<T>
    {
        [JsonProperty("GroupId")]
        public long GroupId { get; set; }
    }

    public class BaseGroupDictionaryItem<T>
    {
        [JsonProperty("GroupName")]
        public string GroupName { get; set; }
        [JsonProperty("Details")]
        public Dictionary<int, T> Details { get; set; }

        public BaseGroupDictionaryItem()
        {
            Details = new Dictionary<int, T>();
        }

        public BaseGroupDictionaryItem(BaseGroupDictionaryItem<T> group) : this()
        {
            this.GroupName = group.GroupName;
        }
    }

    public class ProductionCalendarGroupItem<T>: BaseGroupItem<T>
    {
        public ProductionCalendarGroupItem(BaseGroupItem<T> group) : base(group)
        {
        }

        public ProductionCalendarGroupItem(): base()
        {

        }

        [JsonProperty("IsOverdue")]
        public bool IsOverdue { get; set; }
    }
}
