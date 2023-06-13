namespace Mitrol.Framework.Domain.Bus.Events
{
    using Mitrol.Framework.Domain.Models;
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class ToastNotification : Event
    {
        [JsonConstructor]
        public ToastNotification([JsonProperty("Message")] LocalizedText localizedText, [JsonProperty("IsError")] bool isError)
            : base()
        {
            Message = localizedText;
            IsError = isError;
        }

        public ToastNotification(string localizationKey, bool isError = false)
            : this(new LocalizedText(localizationKey), isError)
        {
        }

        public ToastNotification(string localizationKey, Dictionary<string, string> arguments, bool isError = false)
            : this(new LocalizedText(localizationKey, arguments), isError)
        {
        }

        [JsonProperty("Message")]
        public LocalizedText Message { get; }

        [JsonProperty("IsError")]
        public bool IsError { get; }
    }
}
