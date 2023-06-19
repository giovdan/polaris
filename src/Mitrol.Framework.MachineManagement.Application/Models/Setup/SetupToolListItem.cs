namespace Mitrol.Framework.MachineManagement.Application.Models
{
    using Mitrol.Framework.Domain.Enums;
    using Newtonsoft.Json;

    public class ToolForSpindleListItem
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Slot")]
        public short Slot { get; set; }
        [JsonProperty("Code")]
        public string Code { get; set; }
        [JsonProperty("LocalizationKey")]
        public string LocalizationKey { get; set; }
        [JsonProperty("IsEnabled")]
        public bool IsEnabled { get; set; }
        [JsonProperty("Percentage")]
        public int? Percentage { get; set; }

        public ToolForSpindleListItem()
        {
            IsEnabled = true;
        }
    }

    /// <summary>
    /// Modello 
    /// </summary>
    public class SetupToolListItem: ToolForSpindleListItem
    {
        [JsonProperty("SetupAction")]
        public SetupActionEnum SetupAction { get; set; }
        [JsonProperty("IsMounted")]
        public bool IsMounted { get; set; } //Utensile Montato nel mandrino
        [JsonProperty("IsUsed")]
        public bool IsUsed { get { return SetupAction != SetupActionEnum.NotUsed; } }
        [JsonProperty("ActionLocalizationKey")]
        public string ActionLocalizationKey => $"LBL_ACTION_{SetupAction.ToString().ToUpper()}";

        public SetupToolListItem()
        {
            SetupAction = SetupActionEnum.NotUsed;
            IsMounted = false;
        }

    }

    
}
