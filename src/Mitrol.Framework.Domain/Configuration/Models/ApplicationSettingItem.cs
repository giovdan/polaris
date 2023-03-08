namespace Mitrol.Framework.Domain.Configuration.Models
{
    using Mitrol.Framework.Domain.Configuration.Enums;
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ApplicationSettingItem
    {
        public ApplicationSettingItem()
        {

        }

        public ApplicationSettingItem(ApplicationSettingKeyEnum settingKey, string value)
        {
            SettingKey = settingKey;
            Value = value;
        }

        public string Code
        {
            set
            {
                SettingKey = Enum.TryParse<ApplicationSettingKeyEnum>(value, out var key) ? key : 0;
            }
        }

        [JsonProperty("SettingKey")]
        public ApplicationSettingKeyEnum SettingKey { get; set; }
        [JsonProperty("Value")]
        public string Value { get; set; }
    }

    public static class ApplicationSettingItemExtensions
    {
        public static ApplicationSettingItem Get(this IEnumerable<ApplicationSettingItem> applicationSettings, ApplicationSettingKeyEnum key)
            => applicationSettings?.SingleOrDefault(item => item.SettingKey == key);
    }
}
